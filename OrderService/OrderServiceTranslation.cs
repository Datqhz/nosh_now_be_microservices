using System.ComponentModel;

namespace OrderService;

public enum OrderServiceTranslation
{
    // common
    CMC_ERR_01,
    
    // Exception
    [Description("An unknown error has occurred")]
    EXH_ERR_01,
    
    
    // User
    [Description("Customer not found")]
    CUS_ERR_01,
    
    [Description("Restaurant not found")]
    RES_ERR_01,

}