using System;
using System.Collections.Generic;

public class MusicController
{
    Dictionary<string, string> uriDictionary;
    Dictionary<string, string> playlistDictionary;

    const string angryURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DX2LTcinqsO68";
    const string angryPlaylist = "Old School Metal";

    const string contemptURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DWYBO1MoTDhZI";
    const string contemptPlaylist = "Good Vibes";

    const string disgustURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DX7gIoKXt0gmx";
    const string disgustPlaylist = "All The Feels";

    const string fearURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DX4fpCWaHOned";
    const string fearPlaylist = "Confidence Boost";

    const string happinessURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DWSqmBTGDYngZ";
    const string happinessPlaylist = "Songs to Sing in the Shower";

    const string neutralURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DX4WYpdgoIcn6";
    const string neutralPlaylist = "Chill Hits";

    const string sadnessURI = "https://open.spotify.com/user/spotify/playlist/5eSMIpsnkXJhXEPyRQCTSc";
    const string sadnessPlaylist = "Chill Hits";

    const string surpriseURI = "https://open.spotify.com/user/spotify/playlist/37i9dQZF1DWYs83FtTMQFw";
    const string surprisePlaylist = "Hot Rhythmic";


    public MusicController()
	{
        uriDictionary = new Dictionary<string, string>();
        uriDictionary.Add("anger", angryURI);
        uriDictionary.Add("contempt", contemptURI);
        uriDictionary.Add("disgust", disgustURI);
        uriDictionary.Add("fear", fearURI);
        uriDictionary.Add("happiness", happinessURI);
        uriDictionary.Add("neutral", neutralURI);
        uriDictionary.Add("sadness", sadnessURI);
        uriDictionary.Add("surprise", surpriseURI);

        playlistDictionary = new Dictionary<string, string>();
        playlistDictionary.Add("anger", angryPlaylist);
        playlistDictionary.Add("contempt", contemptPlaylist);
        playlistDictionary.Add("disgust", disgustPlaylist);
        playlistDictionary.Add("fear", fearPlaylist);
        playlistDictionary.Add("happiness", happinessPlaylist);
        playlistDictionary.Add("neutral", neutralPlaylist);
        playlistDictionary.Add("sadness", sadnessPlaylist);
        playlistDictionary.Add("surprise", surprisePlaylist);
    }

    public string getMusicURI(string key)
    {
        string value;
        uriDictionary.TryGetValue(key, out value);
        return value;
    }

    public string getMusicPlaylist(string key)
    {
        string value;
        playlistDictionary.TryGetValue(key, out value);
        return value;
    }
}
