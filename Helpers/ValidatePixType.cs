namespace me_faz_um_pix.Helpers;

public class ValidatePixType
{
    public static bool ValidateType(string type)
    {
        return type switch
        {
            "CPF" => true,
            "Email" => true,
            "Phone" => true,
            "Random" => true,
            _ => false,
        };
    }
}