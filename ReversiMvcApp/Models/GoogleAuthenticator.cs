using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                ManualEntryKey = FormatKey(setupInfo.ManualEntryKey),
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

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }
    }
}
