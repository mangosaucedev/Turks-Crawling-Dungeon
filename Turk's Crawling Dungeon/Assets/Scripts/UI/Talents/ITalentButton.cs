using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TCD.Objects.Parts.Talents;

namespace TCD.UI
{
    public interface ITalentButton : IEventSystemHandler, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler
    {
        void BuildTalent(Talent talent);
    }
}
