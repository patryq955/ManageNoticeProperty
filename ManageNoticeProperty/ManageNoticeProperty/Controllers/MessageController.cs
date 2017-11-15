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
    [Authorize]
    public class MessageController : Controller
    {
        private IExtendRepository<Order> _orderRepository;
        public MessageController(IExtendRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        // GET: Message
        public ActionResult Index()
        {
            List<MessageInfoViewModel> vM = GetListMessageInfoViewModel();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListMessageInfo", vM);
            }

            return View(vM);
        }

        public ActionResult MessageInfo(int id)
        {
            string userID = User.Identity.GetUserId();
            MessageInfoViewModel messageInfo = new MessageInfoViewModel();
            messageInfo.Order = _orderRepository.GetIdAll(id);
            if (messageInfo == null
    || (messageInfo.Order.BuyUserID != userID && messageInfo.Order.Flat.UserId != userID)
    || (IsBuyer(messageInfo.Order) && messageInfo.Order.isDeleteBuyer)
    || (!IsBuyer(messageInfo.Order) && messageInfo.Order.isDeleteSeller)
    )
            {
                return View("EmptyMessage");
            }
            messageInfo.Text = messageInfo.Order.BuyUserID == userID ? TyPeText.Buyer : TyPeText.Seller;
            messageInfo.IsSellOffer = messageInfo.Order.BuyUserID == userID ? false : true;
            updateReadMessage(messageInfo.Order);
            return View(messageInfo);
        }

        public ActionResult DeleteMessage(int id)
        {
            var messageToDelete = _orderRepository.GetIdAll(id);
            if (IsBuyer(messageToDelete))
            {
                messageToDelete.isDeleteBuyer = true;
                messageToDelete.DeleteBuyerDate = DateTime.Today;
            }
            else
            {
                messageToDelete.isDeleteSeller = true;
                messageToDelete.DeleteSellerDate = DateTime.Today;
            }
            _orderRepository.Update(messageToDelete);
            _orderRepository.Save();
            updateReadMessage(messageToDelete);


            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult RefreshMessageIsNotRead()
        {
            return PartialView("_LoginMessagePartial");
        }

        #region private Method
        private List<MessageInfoViewModel> GetListMessageInfoViewModel()
        {
            string userId = User.Identity.GetUserId();
            Func<Order, bool> predicateSellInfo = x => x.Flat.UserId == userId && x.isDeleteSeller == false;
            var orderSellInfo = _orderRepository.GetOverviewAll(predicateSellInfo);

            Func<Order, bool> predicateBuyInfo = x => x.BuyUserID == userId && x.isDeleteBuyer == false;
            var orderBuyInfo = _orderRepository.GetOverviewAll(predicateBuyInfo);
            var buyList = orderBuyInfo.Select(x => new MessageInfoViewModel()
            {
                Order = x,
                Text = TyPeText.Buyer,
                IsSellOffer = false
            }).ToList();

            var sellList = (orderSellInfo.Select(x => new MessageInfoViewModel()
            {
                Order = x,
                Text = TyPeText.Seller,
                IsSellOffer = true
            }).ToList());

            return buyList.Concat(sellList).ToList();
        }

        public bool IsBuyer(Order order)
        {
            return User.Identity.GetUserId() == order.BuyUserID ? true : false;
        }

        public void updateReadMessage(Order order)
        {
            order.isReadSeller = true;
            _orderRepository.Update(order);
            _orderRepository.Save();
        }


        #endregion
    }
}