using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using System.Linq;
using Gangwar.DbModels.Models;
using Gangwar.ServerModels;
using Gangwar.Systems.OneVsOneSystem;

namespace Gangwar.Systems.CommandSystem.AdminCommands
{
    public class OvoCommand : Script
    {
        [Command("1vs1")]
        public static void OvoCMD(Player player, Player receiver, int rounds)
        {
            try
            {
                if (player == null || !player.Exists || receiver == null || !receiver.Exists || rounds > 10) return;

                if(player == receiver)
                {
                    //player.SendChatMessage("[~o~1vs1~w~] Du kannst dir selber keine Anfrage schicken.");
                    player.sendLanguageToPlayer("[~o~1vs1~w~] Du kannst dir selber keine Anfrage schicken.",
                        "[~o~1vs1~w~] You can not send yourself a request.",
                        "[~o~1vs1~w~] Вы не можете отправить себе запрос.");
                    return;
                }

                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                Accounts receiverData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == receiver.SocialClubId);
                if (receiverData == null) return;

                if (accData.IsInOVO)
                {
                    //player.SendChatMessage($"[~o~1vs1~w~] Du bist schon in einem 1vs1.");
                    player.sendLanguageToPlayer($"[~o~1vs1~w~] Du bist schon in einem 1vs1.",
                        $"[~o~1vs1~w~] You are already in a 1vs1.",
                        $"[~o~1vs1~w~] Вы уже в 1vs1.");
                    return;
                }

                if (receiverData.IsInOVO)
                {
                    //player.SendChatMessage($"[~o~1vs1~w~] {receiver.Name} ist bereits in einem 1vs1.");
                    player.sendLanguageToPlayer("[~o~1vs1~w~] {receiver.Name} ist bereits in einem 1vs1.",
                        $"[~o~1vs1~w~] {receiver.Name} is already in a 1vs1.",
                        $"[~o~1vs1~w~] {receiver.Name} уже в 1vs1.");
                    return;
                }

                player.SetData<string>("OVO_REQUEST_NAME_RECEIVER", receiver.Name);
                receiver.SetData<string>("OVO_REQUEST_NAME_SENDER", player.Name);
                
                player.SetData<int>("OVO_ROUNDS", rounds);
                receiver.SetData<int>("OVO_ROUNDS", rounds);
                
                player.SetData<int>("OVO_WIN_COUNT", 0);
                receiver.SetData<int>("OVO_WIN_COUNT", 0);

                //player.SendChatMessage($"[~o~1vs1~w~] Du hast {receiver.Name} eine 1vs1-Anfrage gesendet.");

                player.sendLanguageToPlayer($"[~o~1vs1~w~] Du hast {receiver.Name} eine 1vs1-Anfrage gesendet.",
                    $"[~o~1vs1~w~] You have sent {receiver.Name} a 1vs1 request.",
                    $"[~o~1vs1~w~] Вы отправили {receiver.Name} запрос 1vs1.");

                //receiver.SendChatMessage($"[~o~1vs1~w~] Dir wurde von {player.Name} eine 1vs1-Anfrage gesendet.");

                receiver.sendLanguageToPlayer($"[~o~1vs1~w~] Dir wurde von {player.Name} eine 1vs1-Anfrage gesendet. ({rounds} Runden)",
                    $"[~o~1vs1~w~] You have received a 1vs1 request from {player.Name}. ({rounds} rounds)",
                    $"[~o~1vs1~w~] Вы получили запрос 1vs1 от {player.Name}. ({rounds} runden)");

                //receiver.SendChatMessage($"[~o~1vs1~w~] ~o~/1vs1accept {player.Name} ~w~Um die Anfrage anzunehmen");

                receiver.sendLanguageToPlayer($"[~o~1vs1~w~] ~o~/1vs1accept {player.Name} ~w~Um die Anfrage anzunehmen",
                    $"[~o~1vs1~w~] ~o~/1vs1accept {player.Name} ~w~to accept the request",
                    $"[~o~1vs1~w~] ~o~/1vs1accept {player.Name} ~w~Чтобы принять запрос");
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OvoCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"OvoCMD: {exception.Message}");
            }
        }

        [Command("1vs1accept")]
        public static void OvoAcceptCMD(Player player, Player sender)
        {
            try
            {
                if (player == null || !player.Exists || sender == null || !sender.Exists) return;

                Accounts accData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == player.SocialClubId);
                if (accData == null) return;

                Accounts senderData = ServerAccountModel.Accounts_.FirstOrDefault(x => x.SocialclubId == sender.SocialClubId);
                if (senderData == null) return;

                if (accData.IsInOVO)
                {
                    //player.SendChatMessage($"[~o~1vs1~w~] Du bist schon in einem 1vs1.");
                    player.sendLanguageToPlayer($"[~o~1vs1~w~] Du bist schon in einem 1vs1.",
                        $"[~o~1vs1~w~] You are already in a 1vs1.",
                        $"[~o~1vs1~w~] Вы уже в 1vs1");
                    return;
                }

                if (senderData.IsInOVO)
                {
                    //player.SendChatMessage($"[~o~1vs1~w~] {sender.Name} ist bereits in einem 1vs1");

                    player.sendLanguageToPlayer($"[~o~1vs1~w~] {sender.Name} ist bereits in einem 1vs1",
                        $"[~o~1vs1~w~] {sender.Name} is already in a 1vs1",
                        $"[~o~1vs1~w~] {sender.Name} уже в 1vs1");
                    return;
                }

                if (player.GetData<string>("OVO_REQUEST_NAME_SENDER") == sender.Name &&
                    player.Name == sender.GetData<string>("OVO_REQUEST_NAME_RECEIVER"))
                {
                    //sender.SendChatMessage($"[~o~1vs1~w~] {player.Name} hat deine 1vs1-Anfrage angenommen");

                    sender.sendLanguageToPlayer($"[~o~1vs1~w~] {player.Name} hat deine 1vs1-Anfrage angenommen.",
                        $"[~o~1vs1~w~] {player.Name} has accepted your 1vs1 request.",
                        $"[~o~1vs1~w~] {player.Name} принял ваш запрос 1vs1");

                    player.Dimension = (uint)(senderData.Id * 10);
                    sender.Dimension = (uint)(senderData.Id * 10);
                    
                    player.ResetData("OVO_REQUEST_NAME_SENDER");
                    sender.ResetData("OVO_REQUEST_NAME_SENDER");

                    new Timer(() =>
                    {
                        if (sender == null || !sender.Exists || player == null || !player.Exists) return;
                        OneVsOne.StartOneVsOne(sender, player);
                    }, 1000, "[OvoCommand] Start OvO");
                    return;
                }
                else
                {
                    //player.SendChatMessage($"[~o~1vs1~w~] Dieser Spieler hat dir keine 1vs1-Anfrage geschickt.");

                    player.sendLanguageToPlayer($"[~o~1vs1~w~] Dieser Spieler hat dir keine 1vs1-Anfrage geschickt.",
                        $"[~o~1vs1~w~] This player has not sent you a 1vs1 request.",
                        $"[~o~1vs1~w~] Этот игрок не отправил вам запрос 1vs1.");
                }
            }
            catch (Exception exception)
            {
                Core.SendConsoleMessage($"OvoAcceptCMD: {exception.StackTrace}");
                Core.SendConsoleMessage($"OvoAcceptCMD: {exception.Message}");
            }
        }
    }
}
