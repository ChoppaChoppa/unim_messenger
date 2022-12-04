using System;
using System.Collections.Generic;
using CoreGraphics;
using System.Security.Cryptography.X509Certificates;
using maui.Models;
using maui.Patterns.Commands;
using maui.Views;
using UIKit;
using Xamarin.Forms;

namespace maui.Controllers
{
	public class UserCardController
	{
		private UserCardView userCardView;
        private FirstViewController view;

		public UserCardController(ref FirstViewController View, UserCardView UserCard)
		{
			userCardView = UserCard;
            view = View;
		}
	}
}

