﻿using System.Net;

namespace OpenShock.Desktop.RepositoryServer.Problems;

public sealed class ExceptionProblem : OpenShockProblem
{
    public ExceptionProblem() : base("Exception", "An unknown exception occurred", HttpStatusCode.InternalServerError, "An unknown error occurred. Please try again later. If the issue persists reach out to support.")
    {
    }
}