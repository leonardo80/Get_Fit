using System;
using System.Collections.Generic;
using System.Text;

namespace GetFit.Shared.Class
{
    class Session
    {
        private static Member memberLogin { get; set; }

        public void setMemberLogin(Member member)
        {
            memberLogin = member;
        }

        public Member getMemberLogin()
        {
            return memberLogin;
        }
    }
}
