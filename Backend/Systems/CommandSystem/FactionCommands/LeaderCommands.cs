using System;
using Gangwar.DbModels.Models;
using GTANetworkAPI;

namespace Gangwar.Systems.CommandSystem.FactionCommands
{
    public class LeaderCommands : Script
    {
        [Command("invite")]
        public static void OnPlayerInvite(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                Accounts playerData = player.getData();
                if (playerData == null) return;
                
                Accounts targetData = target.getData();
                if (targetData == null) return;

                if (playerData.PrivateFrakRank < 1)
                {
                    player.sendLanguageToPlayer("Dein Rang ist zuniedrig um jemanden einzuladen!",
                        "Your Rank is too low to invite someone", "ja hier rank zu niedrig und so");
                    return;
                }

                target.SetData("INVITE_REQUEST", true);
                target.SetData("INVITE_REQUEST_ID", playerData.PrivateFrakId);
                target.sendLanguageToPlayer("Du wurdest in eine Fraktion eingeladen! (/acceptinvite)",
                    "You've been invited in a faction (/acceptinvite)",
                    "dings hier, wurdest eingeladen (/acceptinvite)");

                player.sendLanguageToPlayer($"Du hast {target.Name} eingeladen!", $"You've invited {target.Name}!",
                    $"dings hier {target.Name} eingladen :)");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerInvite: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerInvite: {exception.Message}");
            }
        }

        [Command("acceptinvite")]
        public static void OnPlayerAcceptInvite(Player player)
        {
            try
            {
                if (player == null || !player.Exists || !player.HasData("INVITE_REQUEST") ||
                    !player.GetData<bool>("INVITE_REQUEST") || !player.HasData("INVITE_REQUEST_ID")) return;

                Accounts accounts = player.getData();
                if (accounts == null) return;

                player.sendLanguageToPlayer("Du bist der Fraktion beigetreten!", "You have joined the faction!",
                    "dings hier, bist beigetreten :D");
                
                accounts.IsPrivateFrak = true;
                accounts.PrivateFrakId = player.GetData<int>("INVITE_REQUEST_ID");
                accounts.PrivateFrakRank = 0;

                player.ResetData("INVITE_REQUEST");
                player.ResetData("INVITE_REQUEST_ID");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.Message}");
            }
        }

        [Command("uprank")]
        public static void OnPlayerUprank(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                Accounts playerData = player.getData();
                if (playerData == null) return;

                Accounts targetData = target.getData();
                if (targetData == null) return;

                if (playerData.PrivateFrakRank >= 2 && playerData.PrivateFrakRank > targetData.PrivateFrakRank)
                {
                    targetData.PrivateFrakRank++;
                    
                    player.sendLanguageToPlayer($"Du hast {target.Name} upranked!", $"You've upranked {target.Name}",
                        $"Dings hier, uprank bei {target.Name}");
                    
                    target.sendLanguageToPlayer("Du wurdest geupranked!", "You've been upranked",
                        "Dings hier, upranked");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.Message}");
            }
        }

        [Command("frakkick")]
        public static void OnPlayerFrakKick(Player player, Player target)
        {
            try
            {
                if (player == null || !player.Exists || target == null || !target.Exists) return;

                Accounts playerData = player.getData();
                if (playerData == null) return;

                Accounts targetData = target.getData();
                if (targetData == null) return;

                if (playerData.PrivateFrakRank >= 2 && playerData.PrivateFrakRank > targetData.PrivateFrakRank)
                {
                    targetData.PrivateFrakRank = 0;
                    targetData.IsPrivateFrak = false;
                    targetData.PrivateFrakId = 0;
                    
                    player.sendLanguageToPlayer($"Du hast {target.Name} rausgeworfen!", $"You've kicked {target.Name}",
                        $"Dings hier, {target.Name} gekicked");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.StackTrace}");
                Core.SendConsoleMessage($"OnPlayerAcceptInvite: {exception.Message}");
            }
        }
    }
}