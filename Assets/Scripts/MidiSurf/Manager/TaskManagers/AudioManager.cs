using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.IO;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour, ManagerInterface
{
    public ManagerStatus status { get; private set; }

    [SerializeField] internal AudioSource audioSource;
    private string folderPath;

    private List<AudioClip> songs = new List<AudioClip>();
    private int songsCount;
    private int loadedSongs = 0;

    public void Startup()
    {
        folderPath = Application.persistentDataPath + "/track/";
        audioSource.Stop();
        SongManager.onNotesReady += (List<float> list) =>
        {
            audioSource.Play();
        };
        status = ManagerStatus.Started;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            LoadNewTrack();
        }
    }

    private void LoadNewTrack()
    {
        audioSource.Pause();
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            GetSongsFromFolder();
            StartCoroutine(startSong());
        }
        catch (IOException ex) 
        {
            Debug.LogError(ex.Message);
        } 
    }

    private void GetSongsFromFolder()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
        FileInfo[] songFiles = directoryInfo.GetFiles("*.*");

        songsCount = songFiles.Length;
        foreach (FileInfo songFile in songFiles)
        {
            StartCoroutine(ConvertFilesToAudioClip(songFile));
        }

    }

    private IEnumerator ConvertFilesToAudioClip(FileInfo songFile)
    {
        if (songFile.Name.Contains("meta"))
            yield break;
        else
        {
            string songName = songFile.FullName.ToString();
            string url = string.Format("file://{0}", songName);

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.ConnectionError)
                {
                    songs.Add(DownloadHandlerAudioClip.GetContent(www));
                }
                else
                {
                    Debug.Log(www.error);
                }
            }
            loadedSongs++;
        }
    }

    private IEnumerator startSong()
    {
        while(songsCount != loadedSongs)
        {
            yield return new WaitForEndOfFrame();
        }
        audioSource.clip = songs[0];
        audioSource.Play();
    }
}
