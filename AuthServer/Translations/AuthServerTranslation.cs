using System.ComponentModel;

namespace AuthServer.Translations;

public enum AuthServerTranslation
{
    // common
    CMC_ERR_,
    
    // Exception
    [Description("An unknown error has occurred")]
    EXH_ERR_01,
    
    // confirm email
    [Description("Your email already verified")]
    VRE_ERR_01,
    
    [Description("Some error has occurred when verify your email")]
    VRE_ERR_02,
    
    // account
    [Description("Account not found")]
    ACC_ERR_01
}