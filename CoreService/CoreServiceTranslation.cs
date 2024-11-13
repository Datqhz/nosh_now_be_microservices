using System.ComponentModel;

namespace CoreService;

public enum CoreServiceTranslation
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
    
    // Location
    [Description("Location not found")]
    LOC_ERR_01,
    
    [Description("This location is not your own")]
    LOC_ERR_02
}