using CVARC.V2;
using HoMM.Actor;
using HoMM.Commands;
using HoMM.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Rules
{
    public class HommLogicPartHelper : LogicPartHelper
    {
        int playersCount;

        public HommLogicPartHelper(int playersCount)
        {
            this.playersCount = playersCount;
        }

        public override LogicPart Create()
        {
            var logicPart = new LogicPart();
            var rules = new HommRules();

            logicPart.CreateWorld = () => new HommWorld();
            logicPart.CreateDefaultSettings = () => new Settings { OperationalTimeLimit = 5, TimeLimit = 90 };

            logicPart.WorldStateType = typeof(HommWorldState);
            logicPart.CreateWorldState = seed => new HommWorldState(int.Parse(seed));
            logicPart.PredefinedWorldStates.AddRange(Enumerable.Range(0, 5).Select(i => i.ToString()));

            var actorFactory = ActorFactory.FromRobot(new HeroRobot(), rules);
            logicPart.Actors[TwoPlayersId.Left] = actorFactory;

            if (playersCount == 2)
                logicPart.Actors[TwoPlayersId.Right] = actorFactory;

            logicPart.Bots[HommRules.StandingBotName] = () => new Bot<HommCommand>(_ => new HommCommand());

            return logicPart;
        }
    }
}
