﻿using System;
using System.Web.Mvc;
using BlipCo.Models;

namespace BlipCo.Controllers
{
    public class ButtonsController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            // Create a new user object to hold the data to be displayed on the form
            // and assign a GUID for the user during initialization.
            var user = new User()
            {
                UserID = Guid.NewGuid().ToString()
                // UserID is passed to the Razor form as a string so it can be conveniently passed back on submit.
                // GUID's would be coerced to strings when the form is rendered, but would not be converted from
                // strings to GUID's on the POST action.
            };

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserID,Username")] User user, float? answer)
        {
            if (ModelState.IsValid && answer.HasValue)
            {
                if (answer > 2)
                {
                    user.TermsStatus = "not accepted";
                }

                //switch (answer)
                //{
                //    case 1.1f:
                //        user.TermsAcceptedOn = DateTime.Now;
                //        user.TermsStatus = "Accepted";
                //        break;
                //    case 2.2f:
                //        user.TermsStatus = "Declined";
                //        break;
                //    case 3.3f:
                //        user.TermsStatus = "Deferred";
                //        break;
                //    default:
                //        user.TermsStatus = "Deferred";
                //        break;
                //}

                // Code to save the values for user.Username and user.AcceptedOn to permanent storage could go here.
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TermsAccept([Bind(Include = "UserID,Username")] User user)
        {
            if (ModelState.IsValid)
            {
                user.TermsAcceptedOn = DateTime.Now;
                user.TermsStatus = "Accepted";
            }
            return View("Index", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TermsDecline()
        {
            if (ModelState.IsValid)
            {
                //user.TermsStatus = "Declined"; Controller actions matching `formaction` attribute values
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
