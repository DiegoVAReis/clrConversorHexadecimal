using System;
using System.Text;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static string clrConversorHexadecimal(string sTextConvert, string sTypeConvert, string sSeparator)
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                         //
        //  Função Main --> Responsável por direcionar o Fluxo para as outras funções internas..    //
        //  Desenvolvido por: https://github.com/DiegoVAReis                                       //
        //                                                                                         //
        /////////////////////////////////////////////////////////////////////////////////////////////

        string sReturn = "";

        Regex regSeparatorsValid = new Regex(@"^[_\-\:\;\|]{1,1}$");

        try
        {

            if (regSeparatorsValid.IsMatch(sSeparator))
            {
                if (sTypeConvert == "convertHex")
                {
                    // --> CONVERTER EM HEXADECIMAL
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    // --> Primeiro converte tudo para string para depois converter para hexadecimal, assim mantem toda a string num mesmo formato.
                    sTextConvert = ConvertHexadecimalToString(sTextConvert, sSeparator);

                    // --> Após tudo estar na mesma base é convertido em hexadecimal separando pelo separador passado (um sepador válido)
                    sReturn = ConvertStringToHexadecimal(sTextConvert, sSeparator);
                }
                else if (sTypeConvert == "convertString")
                {   
                    // --> CONVERTER EM STRING
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    // --> Convertendo o Hexadecimal para string
                    sReturn = ConvertHexadecimalToString(sTextConvert, sSeparator);

                }
                else if (sTypeConvert == "dev" || sTypeConvert == "copyright")
                {
                    sReturn = "Desenvolvido por https://github.com/DiegoVAReis";
                }
                else
                {
                    sReturn = "Tipo de conversão incorreto";
                }
            }
            else
            {
                sReturn = "Tipo de separador incorreto, apenas os separadores (_ - : ; |) são válidos.";
            }

            return sReturn;
        }
        catch
        {
            // --> Se der algum erro no processo devolve o mesmo texto que foi passado inicialmente.
            return sTextConvert;
        }
                
    }

    private static string ConvertStringToHexadecimal(string sTextConvert, string sSeparator)
    {
        ///////////////////////////////////////////////////////
        //                                                   //
        // --> Converter a string passada em hexadecimal..   //
        //                                                   //
        ///////////////////////////////////////////////////////

        string sHexReturn = "";

        // --> Transforma a string num array de Bytes
        byte[] arrayBytes = System.Text.Encoding.UTF8.GetBytes(sTextConvert);

        // --> Percorre a string e vai transformando em hexadecimal.
        foreach (char c in arrayBytes)
        {
            sHexReturn += sSeparator + ((int)c).ToString("x");
        }

        return sHexReturn.ToUpper();
    }
    
    private static string ConvertHexadecimalToString(string sTextConvert, string sSeparator)
    {

        ///////////////////////////////////////////////////////////
        //                                                       //
        // --> Converte do Hexadecimal para a string novamente   //
        //                                                       //
        ///////////////////////////////////////////////////////////

        string sTextConvertAux = sTextConvert;
        string hexadecimalAux = "";
        string hexadecimalString = "";
        string sTextReturn = "";

        int position = 0;

        while (position < sTextConvertAux.Length)
        {
            if ((sTextConvertAux.Length - position) >= 3)
            {
                hexadecimalAux = sTextConvertAux.Substring(position, 3);
            }
            else
            {
                hexadecimalAux = sTextConvertAux.Substring(position);
            }

            hexadecimalString = "";

            if (isHexadecimal(hexadecimalAux, sSeparator))
            {

                while (isHexadecimal(hexadecimalAux, sSeparator))
                {

                    hexadecimalString = hexadecimalString + hexadecimalAux;
                    position = position + 3;

                    if ((sTextConvertAux.Length - position) >= 3)
                    {
                        hexadecimalAux = sTextConvertAux.Substring(position, 3);
                    }
                    else
                    {
                        hexadecimalAux = sTextConvertAux.Substring(position);
                    }

                    if (position > sTextConvertAux.Length)
                    {
                        break;
                    }

                }

                sTextReturn = sTextReturn + HexadecimalStringToString(hexadecimalString);

            }
            else
            {
                sTextReturn = sTextReturn + sTextConvertAux[position].ToString();
                position++;

            }

        }

        return sTextReturn;

    }

    private static string HexadecimalStringToString(string sTextHex)
    {

        ////////////////////////////////////////////////////////////////
        //                                                            //
        // --> Função para converter o Hexadecimal em String.         //
        //                                                            //
        ////////////////////////////////////////////////////////////////

        sTextHex = sTextHex.Replace("_", "");

        byte[] rawByte = new byte[sTextHex.Length / 2];

        for (int i = 0; i < rawByte.Length; i++)

        {

            rawByte[i] = Convert.ToByte(sTextHex.Substring(i * 2, 2), 16);

        }

        byte[] dataByte = rawByte;

        string s = Encoding.UTF8.GetString(dataByte);

        return s;
    }

    private static bool isHexadecimal(string sText, string sSeparator)
    {
        ////////////////////////////////////////////////////////////////
        //                                                            //
        // --> Função que valide se o texto passado é um hexadecimal  //
        //                                                            //
        ////////////////////////////////////////////////////////////////

        // --> Expressão regular com os caracteres aceitos de um hexadecimal.
        Regex regHexadecimal = new Regex(@"^[0-9A-Fa-f]");

        // --> Como colocamos um separador de 1 caractere, teria que ter no minimo 3 dígitos para ser hexadecimal
        if (sText.Length == 3)
        {
            return (sText[0].ToString() == sSeparator) && regHexadecimal.IsMatch(sText[1].ToString()) && regHexadecimal.IsMatch(sText[2].ToString());
        }
        else
        {
            return false;
        }

    }

}
