//
// Script name: XMLToTrackConverter
//
//
// Programmer: Kentaurus
//

using UnityEngine;
using System.Collections.Generic;
using Track;

public class XMLToTrackConverter
{
    #region Variables
    private XMLContainer<Tile> m_Tiles = new XMLContainer<Tile>();
    private Dictionary<string, List<TrackInformation>> m_Tracks = new Dictionary<string, List<TrackInformation>>();

    public Dictionary<string, List<TrackInformation>> Tracks
    {
        get { return m_Tracks; }
    }
    #endregion

    #region Constructor
    public XMLToTrackConverter() { }
    #endregion

    #region Public Methods
    /// <summary>
    /// Loads an xml to convert to track pieces
    /// do not put the .xml in the file name
    /// </summary>
    /// <param name="filename"></param>
    public Dictionary<string, List<TrackInformation>> Load(string filename)
    {
        m_Tiles = XMLContainer<Tile>.Load(filename);
        TrackInformation information = null;

        foreach (Tile tile in m_Tiles.m_Blocks)
        {
            foreach (Vector3 vector in tile.Positions)
            {
                information = new TrackInformation();
                information.Position = vector;

                if (!m_Tracks.ContainsKey(tile.PrefabName))
                {
                    m_Tracks.Add(tile.PrefabName, new List<TrackInformation>());
                }

                m_Tracks[tile.PrefabName].Add(information);
            }
        }

        return m_Tracks;
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

}
