/*
    Copyright 2012 MCForge
 
    Dual-licensed under the	Educational Community License, Version 2.0 and
    the GNU General Public License, Version 3 (the "Licenses"); you may
    not use this file except in compliance with the Licenses. You may
    obtain a copy of the Licenses at
    
    http://www.opensource.org/licenses/ecl2.php
    http://www.gnu.org/licenses/gpl-3.0.html
    
    Unless required by applicable law or agreed to in writing,
    software distributed under the Licenses are distributed on an "AS IS"
    BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
    or implied. See the Licenses for the specific language governing
    permissions and limitations under the Licenses.
 */
using System;
using System.IO;
namespace MCForge
{
    public sealed class BetaCraftBeat2 : IBeat
    {
        public bool Url2Said = false;

        public string URL
        {
            get
            {
                return "http://betacraft.uk/heartbeat.jsp";
            }
        }

        public bool Persistance
        {
            get { return true; }
        }

        public string Prepare()
        {
            return "&port=" + Server.port +
                "&max=" + Server.players +
                "&name=" + Heart.EncodeUrl(Server.name) +
                "&public=" + Server.pub +
                "&version=7" +
                "&salt=" + Server.salt +
                "&users=" + Player.players.Count +
                "&software=" + Server.SoftwareNameVersioned2;
        }

        public void OnResponse(string line)
        {

            // Only run the code below if we receive a response
            if (!String.IsNullOrEmpty(line.Trim()))
            {
                string newHash = line.Substring(line.LastIndexOf('/') + 1);

                // Run this code if we don't already have a hash or if the hash has changed
                if (String.IsNullOrEmpty(Server.Hash) || !newHash.Equals(Server.Hash))
                {
                    File.WriteAllText("text/BC2externalurl.txt", Server.BCURL2);
                    if (Url2Said == false)
                    {
                        Server.s.Log("BetaCraft2 URL found: " + Server.BCURL2);
                        Url2Said = true;
                    }
                }
            }
        }
    }
}
