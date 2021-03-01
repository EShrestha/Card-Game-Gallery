using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Card_Game_Gallery.Games.Go_Fish
{

    // Holds all of the logic for Go Fish, such as winCheck...
    public static class GoFishLogic
    {
        private static object saveGamePath;


        // Saves teh current state of the game
        public static bool SaveGame(GoFishSaveGame save, string saveGamePath)
        {
            try
            {
                // Save files name is the current time
                string time = DateTime.Now.ToString("T");

                time = time.Replace(':', '-');

                IFormatter formatter = new BinaryFormatter();
                System.IO.Directory.CreateDirectory(@"\CardGameGallery\Go Fish");
                string path = saveGamePath.Equals("") ? @$"\CardGameGallery\Go Fish\{time}.GoFish" : saveGamePath;
                Stream stream = new FileStream(@$"{path}", FileMode.Create, FileAccess.Write);

                formatter.Serialize(stream, save);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
