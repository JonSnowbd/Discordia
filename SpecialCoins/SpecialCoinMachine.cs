using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCoins
{
    public sealed class SpecialCoinMachine
    {
        public List<SpecialUser> Users = new List<SpecialUser>();

        [JsonIgnore]
        private string JSONPath = System.Environment.CurrentDirectory+"/SpecialCoins.json";

        public void LoadOrCreate()
        {
            if (!File.Exists(JSONPath))
            {
                using (File.Create(JSONPath)) { };
            }else
            {
                SpecialCoinMachine deserialized;
                deserialized = JsonConvert.DeserializeObject<SpecialCoinMachine>(File.ReadAllText(JSONPath));
                try
                {
                    Users = deserialized.Users;
                }catch
                {
                    return;
                }
                
            }
        }

        public void UpdateToJson()
        {
            if (!File.Exists(JSONPath)) { return; }

            string Content = JsonConvert.SerializeObject(this);

            System.IO.File.WriteAllText(JSONPath, Content);
        }

        public void GiveCoinToUser(ulong ID, string Note)
        {
            foreach(SpecialUser usr in Users)
            {
                if(usr.ID != ID) { continue; }

                usr.Coins = usr.Coins + 1;
                usr.RecentNotes[1] = usr.RecentNotes[0];
                usr.RecentNotes[0] = Note;

                UpdateToJson();
                return;
            }

            // Not found, make new person.
            Users.Add(new SpecialUser {
                ID = ID
            });
            // Restart this with the new person.
            GiveCoinToUser(ID, Note);
        }

    }
}
