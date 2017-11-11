using ManageNoticeProperty.Models;
using ManageNoticeProperty.Models.Repository;
using ManageNoticeProperty.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageNoticeProperty.Controllers
{
    public class MessageController : Controller
    {
        IExtendRepository<Order> _orderRepository;
        public MessageController(IExtendRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // GET: Message
        public ActionResult Index()
        {
            List<MessageInfoViewModel> vM = getListMessageInfoViewModel();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListMessageInfo", vM);
            }

            return View(vM);
        }

        public ActionResult MessageInfo(int id)
        {
            MessageInfoViewModel messageInfo = new MessageInfoViewModel();
            messageInfo.Order = _orderRepository.GetIdAll(id);
            var userId = User.Identity.GetUserId();
            if (messageInfo == null || (messageInfo.Order.BuyUserID != userId && messageInfo.Order.Flat.UserId != userId)
                || (messageInfo.Order.isDelete = true && messageInfo.Order.Flat.UserId != userId))
            {
                return View("EmptyMessage");
            }
            messageInfo.Text = messageInfo.Order.BuyUserID == userId ? TyPeText.Buyer : TyPeText.Seller;
            messageInfo.IsSellOffer = messageInfo.Order.BuyUserID == userId ? false : true;

            return View(messageInfo);
        }


        public ActionResult DeleteMessage(int id)
        {
             var messageToDelete = _orderRepository.GetIdAll(id);
            messageToDelete.isDelete = true;
            _orderRepository.Update(messageToDelete);
            _orderRepository.Save();



            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        #region private Method
        private List<MessageInfoViewModel> getListMessageInfoViewModel()
        {
            Func<Order, bool> predicateSellInfo = x => x.Flat.UserId == User.Identity.GetUserId()
         && x.isDelete == false;
            var orderSellInfo = _orderRepository.GetOverviewAll(predicateSellInfo);

            Func<Order, bool> predicateBuyInfo = x => x.BuyUserID == User.Identity.GetUserId();
            var orderBuyInfo = _orderRepository.GetOverviewAll(predicateBuyInfo);
            var buyList = orderBuyInfo.Select(x => new MessageInfoViewModel()
            {
                Order = x,
                Text = TyPeText.Buyer
            }).ToList();

            var sellList = (orderSellInfo.Select(x => new MessageInfoViewModel()
            {
                Order = x,
                Text = TyPeText.Seller
            }).ToList());

            return buyList.Concat(sellList).ToList();
        }
        #endregion
    }
}