using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

namespace WpfSoundApp.Core
{
    public static class FirebaseManager
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory + @"arukgamehub-firebase-adminsdk-vzug1-91f69f47bc.json";
        static FirestoreDb db;
        private static string tempVersion = "";

        public static async Task LoadAppData()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("arukgamehub");
            Query docref = db.Collection("AppData").OrderBy("Version").LimitToLast(1);
            QuerySnapshot snapshot = await docref.GetSnapshotAsync();
            await Task.Run(() =>
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    tempVersion = data.First().Value.ToString();
                }
            });

        }

        public static string GetCurrentAppVerion()
        {
            return tempVersion;
        }

        //
        private static List<Dictionary<string, object>> tempPanData = new List<Dictionary<string, object>>();
        public static List<Dictionary<string, object>> GetCurrentPanTableData()
        {
            return tempPanData;
        }

        public static async Task LoadTableData(string collection)
        {
            tempPanData = new List<Dictionary<string, object>>();
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("arukgamehub");
            Query docref = db.Collection(collection).OrderBy("Name").LimitToLast(2);
            QuerySnapshot snapshot = await docref.GetSnapshotAsync();
            await Task.Run(() =>
            {
                foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
                {
                    Dictionary<string, object> data = documentSnapshot.ToDictionary();
                    tempPanData.Add(data);
                }
            });

        }
    }
}
