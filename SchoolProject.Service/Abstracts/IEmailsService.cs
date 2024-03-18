﻿namespace SchoolProject.Service.Abstracts
{
    public interface IEmailsService
    {
        public Task<string> SendEmail(string email, string msg, string? reason);
    }
}
