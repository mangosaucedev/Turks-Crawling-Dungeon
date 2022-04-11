using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public interface ITalentButton : IEventSystemHandler, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler
    {
        TalentView View { get; set; }

        Talent Talent { get; }

        int TalentLevel { get; }

        void BuildTalent(Talent talent);
    }
}
