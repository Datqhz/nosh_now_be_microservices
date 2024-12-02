using Shared.Enums;

namespace Shared.Helpers;

public static class IngredientUnitHelper
{
    public static string GetUnitName(this IngredientUnit unit)
    {
        switch (unit)
        {
            case IngredientUnit.Gram:
                return "g";
            case IngredientUnit.Kilogram:
                return "kg";
            case IngredientUnit.Liter:
                return "l";
            case IngredientUnit.Milliliter:
                return "ml";
            default:
                return "NaN";
        }
    }
}