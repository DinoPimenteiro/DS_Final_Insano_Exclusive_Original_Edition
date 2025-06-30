using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cadastroInsano
{
    public partial class Form1 : Form
    {
        // Declaração das variáveis principais;
        decimal saldo = 1000;

        // Declaração das variáveis das notas
        int notas100 = 20, 
            notas50 = 40, 
            notas20 = 25, 
            notas10 = 50;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            saldoINT.Text = $"Saldo: R$ {saldo}";
            NotasTXT.Text = $"Notas disponíveis: \n {notas100} de R$ 100 \n {notas50} de R$ 50 \n {notas20} de R$ 20 \n {notas10} de R$ 10";
        }

        private void DepositarBTN_Click(object sender, EventArgs e)
        {
            string teste = DepositoTXT.Text;
            if (teste.Contains(".")) 
            {
                MessageBox.Show("Não pode.");
                DepositoTXT.Clear();
                Atualizar();
            }

            // Declaração da variável de valor a ser depositado
            decimal valor;
            /* "Se a tentaiva de converter o que foi inserido em DepositoTXT for bem sucedida, 
             o valor convertido será atribuido a variável valor ...*/
            if (decimal.TryParse(DepositoTXT.Text, out valor) && valor > 0 && valor % 10 == 0)
            {

                saldo += valor; // saldo = saldo + valor;
                MessageBox.Show("Depósito feito com sucesso!");
                Atualizar();
                DepositoTXT.Clear();
            }
            else
            {
                MessageBox.Show("Digite um valor válido (múltiplo de 10 e sem caracteres).");
            }
        }

        private void SacarBTN_Click(object sender, EventArgs e)
        {
            string teste = SaqueTXT.Text;
            if (teste.Contains("."))
            {
                MessageBox.Show("Não pode.");
                SaqueTXT.Clear();
                Atualizar();
            }

            decimal valor;
            if (decimal.TryParse(SaqueTXT.Text, out valor) && valor > 0 && valor % 10 == 0)
            {
                if (valor > saldo)
                {
                    MessageBox.Show("Saldo insuficiente.");
                    return;
                }

                int restante = (int)valor;

                int usar100 = Math.Min(restante / 100, notas100);
                restante -= usar100 * 100;

                int usar50 = Math.Min(restante / 50, notas50);
                restante -= usar50 * 50;

                int usar20 = Math.Min(restante / 20, notas20);
                restante -= usar20 * 20;

                int usar10 = Math.Min(restante / 10, notas10);
                restante -= usar10 * 10;

                if (restante == 0)
                {
                    saldo -= valor;
                    notas100 -= usar100;
                    notas50 -= usar50;
                    notas20 -= usar20;
                    notas10 -= usar10;

                    MessageBox.Show($"Saque de R$ {valor} feito com sucesso!\nNotas entregues:\n100: {usar100}, 50: {usar50}, 20: {usar20}, 10: {usar10}");

                    Atualizar();

                    string nomeArquivo = $"comprovante_{DateTime.Now.Ticks}.txt";
                    string conteudo = $"Saque de R$ {valor} em {DateTime.Now}\n" +
                                      $"Notas: 100x{usar100}, 50x{usar50}, 20x{usar20}, 10x{usar10}\n" +
                                      $"Saldo final: R$ {saldo}";

                    try
                    {
                        
                        string caminhoDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        string caminhoCompleto = Path.Combine(caminhoDesktop, nomeArquivo);
                       
                        File.WriteAllText(caminhoCompleto, conteudo);

                     
                        MessageBox.Show($"Comprovante salvo em:\n{caminhoCompleto}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao gerar comprovante:\n{ex.Message}");
                    }

                }
                else
                {
                    MessageBox.Show("O caixa não tem as notas certas para esse saque.");
                }
            }
            else
            {
                MessageBox.Show("Digite um valor válido (múltiplo de 10).");
            }
        }

        private void Atualizar()
        {
            saldoINT.Text = $"Saldo: R$ {saldo}";
            NotasTXT.Text = $"Notas disponíveis: \n {notas100} de R$ 100 \n {notas50} de R$ 50 \n {notas20} de R$ 20 \n {notas10} de R$ 10";
            invisivelTXT.Text = "É sério, esse é o verdadeiro. O resto que copia é tudo invejoso";
        }

        private void AtualizarBTN_Click(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void LimparBTN_Click(object sender, EventArgs e)
        {
            SaqueTXT.Clear();
            DepositoTXT.Clear();
        }

        private void NotasTXT_Click(object sender, EventArgs e)
        {}

        private void saberTXT_Click(object sender, EventArgs e)
        {
            saberTXT.Text = "Exclusivo: Existe somente este de original, o resto é cópia.";
        }

        private void SaqueTXT_TextChanged(object sender, EventArgs e)
        {

        }

        private void DepositoTXT_TextChanged(object sender, EventArgs e)
        {}

        private void saldoINT_Click(object sender, EventArgs e)
        {}

        private void label2_Click(object sender, EventArgs e)
        { }

    }
}
