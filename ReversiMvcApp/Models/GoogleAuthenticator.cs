using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Authenticator;
using Microsoft.AspNetCore.Identity;
using ReversiMvcApp.Controllers;
using ReversiRestAPI.Models;

namespace ReversiMvcApp.Models
{
    public class GoogleAuthenticator
    {
        public PlayerController PlayerController { get; }

        public GoogleAuthenticator(PlayerController playerController)
        {
            PlayerController = playerController;
        }

        public AuthenticatorInfo GenerateQrInfo(Player player)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("ReversiMvc", player.Email, player.TFAKey, false, 2);

            return new AuthenticatorInfo()
            {
                ManualEntryKey = setupInfo.ManualEntryKey,
                QrCoreSetupImageUrl = setupInfo.QrCodeSetupImageUrl
            };
        }

        public async Task GenerateTFAKey(Player player)
        {
            var key = StringHelper.GenerateRandomString(12, false);
            player.TFAKey = key;
            await PlayerController.SavePlayer(player);
        }

        public bool ValidateAuthenticatorCode(Player player, string verificationCode)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            return tfa.ValidateTwoFactorPIN(player.TFAKey, verificationCode);
        }

        public class AuthenticatorInfo
        {
            public string QrCoreSetupImageUrl { get; set; }
            public string ManualEntryKey { get; set; }
        }
    }
}
