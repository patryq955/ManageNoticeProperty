using ManageNoticeProperty.Models;
using System.ComponentModel.DataAnnotations;

namespace ManageNoticeProperty.ViewModel
{
    public class MessageInfoViewModel
    {
        private TyPeText _text;
        public Order Order { get; set; }

        public bool IsSellOffer { get; set; }

        public TyPeText Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public string GetText()
        {
            switch (_text)
            {
                case TyPeText.Buyer:
                    return "Zakupione";
                case TyPeText.Seller:
                    return "Sprzedane";

            }
            return null;
        }


    }

    public enum TyPeText
    {
        [Display(Name = "Sprzedane")]
        Seller,
        [Display(Name = "Zakupione")]
        Buyer
    }
}