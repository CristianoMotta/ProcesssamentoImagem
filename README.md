# PROCESSAMENTO IMAGEM
O trabalho consiste em construir um algoritmo de equalização de imagem em qualquer linguagem de programação. A entrada desse algoritmo será um vetor com a linha de pixels da imagem e a saída é a mesma linha equalizada.

# Url projeto em funcionamento:
https://processamentoimagemp1.azurewebsites.net

# Algoritmo
https://github.com/CristianoMotta/ProcesssamentoImagem/blob/master/Web/Util/AlgoritmoEqualizacao.cs

int atual = 0;

byte[] resultadoEqualizacao = new byte[bytes];
double[] pn = new double[256];

for (int pi = 0; pi < bytes; pi += 4)
{
    pn[vetorDePixels[pi]]++;
}

for (int prob = 0; prob < pn.Length; prob++)
{
    pn[prob] /= (largura * altura);
}

for (int y = 0; y < altura; y++)
{
    for (int x = 0; x < largura; x++)
    {
        atual = y * sd.Stride + x * 4;
        double sum = 0;
        for (int i = 0; i < vetorDePixels[atual]; i++)
        {
            sum += pn[i];
        }
        for (int c = 0; c < 3; c++)
        {
            resultadoEqualizacao[atual + c] = (byte)Math.Floor(255 * sum);
        }
        resultadoEqualizacao[atual + 3] = 255;
    }
}
