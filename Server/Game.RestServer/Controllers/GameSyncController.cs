using System;
using Game.RestServer.Data;
using Microsoft.AspNetCore.Mvc;
using Get = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using Patch = Microsoft.AspNetCore.Mvc.HttpPatchAttribute;

namespace Game.RestServer.Controllers
{
    [ApiController]
    public class GameSyncController
    {
        private readonly GameDataService dataService;

        public GameSyncController(GameDataService dataService)
        {
            this.dataService = dataService;
        }


        [Get("/sessions")]
        public string StartSession()
        {
            var newId = Guid.NewGuid().ToString();
            this.dataService.Initialize(newId);
            return newId.ToString();
            // generate session OTP
            // generate session access token
        }


        [Patch("/sessions/{sessionId}/game-data")]
        public void SendGameData(string sessionId)
        {
            this.dataService.AddPlayers(sessionId, "testPlayer", new[] { new object() });
            this.dataService.AddWords(sessionId, new[] { "testWord" });

            // save players per session
            // save words per session
        }


        [Get("/sessions/{sessionId}/game-data")]
        public dynamic GetGameData(string sessionId)
        {
            return new
            {
                players = this.dataService.GetPlayers(sessionId),
                words = this.dataService.GetWords(sessionId)
            };
            // return all players
            // return all words
        }
    }
}