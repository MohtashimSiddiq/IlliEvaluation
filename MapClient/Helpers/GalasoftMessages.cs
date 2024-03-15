using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Map.Client.Helpers.Enums;
using Map.Common.Enums;
using Prism.Events;

namespace Map.Client.Helpers
{
    public class LocaleMessageEvent:PubSubEvent<eLocales>
    {

    }

    public class UIModeMessageEvent : PubSubEvent<eUIMode>
    {
    }

    public class UIColorMessageEvent : PubSubEvent<eUIColor>
    {
    }

    public class MsgBxResultMessageEvent : PubSubEvent<eMessageBoxResult>
    {

    }

    public class ShowMsgBxMessageEvent : PubSubEvent<Tuple<eMessageBoxType,string,string>>
    {

    }

    public class SoldierAddedEvent:PubSubEvent<Map.Common.Models.Soldier>
    {

    }

}
