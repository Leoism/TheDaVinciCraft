// ----------------------------------------------------------------------------
// <copyright file="CustomTypes.cs" company="Exit Games GmbH">
//   PhotonNetwork Framework for Unity - Copyright (C) 2018 Exit Games GmbH
// </copyright>
// <summary>
// Sets up support for Unity-specific types. Can be a blueprint how to register your own Custom Types for sending.
// </summary>
// <author>developer@exitgames.com</author>
// ----------------------------------------------------------------------------


namespace Photon.Pun
{
  using UnityEngine;
  using Photon.Realtime;
  using ExitGames.Client.Photon;
  using System.Collections.Generic;


  /// <summary>
  /// Internally used class, containing de/serialization method for PUN specific classes.
  /// </summary>
  internal static class CustomTypes
  {
    /// <summary>Register de/serializer methods for PUN specific types. Makes the type usable in RaiseEvent, RPC and sync updates of PhotonViews.</summary>
    internal static void Register()
    {
      Debug.Log(PhotonPeer.RegisterType(typeof(Photon.Realtime.Player), (byte)'P', SerializePhotonPlayer, DeserializePhotonPlayer));
      Debug.Log(PhotonPeer.RegisterType(typeof(Vector3Int), (byte)'A', SerializeVector3Int, DeserializeVector3Int));
      Debug.Log(PhotonPeer.RegisterType(typeof(Vector3Int[]), (byte)'B', SerializeHashset, DeserializeHashset));
    }


    #region Custom De/Serializer Methods

    public static readonly byte[] memPlayer = new byte[4];

    private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
    {
      int ID = ((Photon.Realtime.Player)customobject).ActorNumber;

      lock (memPlayer)
      {
        byte[] bytes = memPlayer;
        int off = 0;
        Protocol.Serialize(ID, bytes, ref off);
        outStream.Write(bytes, 0, 4);
        return 4;
      }
    }

    private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
    {
      if (length != 4)
      {
        return null;
      }

      int ID;
      lock (memPlayer)
      {
        inStream.Read(memPlayer, 0, length);
        int off = 0;
        Protocol.Deserialize(out ID, memPlayer, ref off);
      }

      if (PhotonNetwork.CurrentRoom != null)
      {
        Photon.Realtime.Player player = PhotonNetwork.CurrentRoom.GetPlayer(ID);
        return player;
      }
      return null;
    }

    // 3 ints at 4 bytes a piece
    public static readonly byte[] memVector3Int = new byte[3 * 4];
    private static short SerializeVector3Int(StreamBuffer outStream, object customObj)
    {
      Vector3Int v3i = (Vector3Int)customObj;
      lock (memVector3Int)
      {
        byte[] bytes = memVector3Int;
        int index = 0;
        Protocol.Serialize(v3i.x, bytes, ref index);
        Protocol.Serialize(v3i.y, bytes, ref index);
        Protocol.Serialize(v3i.z, bytes, ref index);
        outStream.Write(bytes, 0, 3 * 4);
      }
      return 3 * 4;
    }

    private static object DeserializeVector3Int(StreamBuffer inStream, short length)
    {
      int x, y, z;
      lock (memVector3Int)
      {
        inStream.Read(memVector3Int, 0, 3 * 4);
        int index = 0;
        Protocol.Deserialize(out x, memVector3Int, ref index);
        Protocol.Deserialize(out y, memVector3Int, ref index);
        Protocol.Deserialize(out z, memVector3Int, ref index);
      }
      return new Vector3Int(x, y, z);
    }

    private static HashSet<Vector3Int> DeserializeHashset(byte[] data)
    {
      HashSet<Vector3Int> result = new HashSet<Vector3Int>();
      for (int i = 3; i < data.Length; i += 3)
      {
        result.Add(new Vector3Int(data[i - 1], data[i - 2], data[i - 3]));
      }
      return result;
    }

    private static byte[] SerializeHashset(object customObj)
    {
      Vector3Int[] hs = (Vector3Int[])customObj;
      byte[] bytes = new byte[hs.Length * 3]; // 3 ints per entry
      int i = 0;
      foreach (Vector3Int v3i in hs)
      {
        bytes[i] = (byte)v3i.x;
        bytes[i + 1] = (byte)v3i.y;
        bytes[i + 2] = (byte)v3i.z;
        i += 3;
      }
      return bytes;
    }

    #endregion
  }
}
