﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Semaphore.Common.Enums
{
	public enum GenericStatusValue
	{
		Success = 1,
		HasErrorMessage = -97,
		Unauthorized = -98,
		UnknownError = -99,
		NoInternetConnection = -100
	}
}
