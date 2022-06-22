using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroDeClientes
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            AtualizarComboBoxPaises();
            CriarControlesEstadosCivis();
            
            DesabilitarCampos();
           
        }

        string Nome;
    private void Infomar (string mensagem)
        {
            MessageBox.Show(mensagem, "Informação", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

    private bool Confirmar (string pergunta)
        {
            return MessageBox.Show(pergunta, "Confirmação", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

        }

    private void AtualizarComboBoxPaises()
        {
            cbxNacionalidade.DataSource = Pais.Listagem;
            cbxNacionalidade.DisplayMember = "";
            cbxNacionalidade.DisplayMember = "Nome";
            cbxNacionalidade.ValueMember = "Sigla";
            cbxNacionalidade.SelectedIndex = -1;
        }

        private void AtualizarComboBoxClientes()
        {
            cbxCliente.DataSource = Cliente.Listagem;
            cbxCliente.DisplayMember = "";
            cbxCliente.DisplayMember = "Nome";
            cbxCliente.ValueMember = "Codigo";
        }

        private void CorrigirTabStop(object sender, EventArgs e)
        {
            ((RadioButton)sender).TabStop = true;
        }

        private void CriarControlesEstadosCivis()
        {
            int iRB = 0;
            var estadosCivis = Enum.GetValues(typeof(EnumEstadoCivil));
            foreach (var ec in estadosCivis)
            {
                RadioButton rb = new RadioButton();
                {
                    Text = ec.ToString();
                    Location = new Point(10, (iRB + 1) * 27);
                    Width = 85;
                    TabStop = true;
                    TabIndex = iRB;
                    Tag = ec;
                };
                rb.TabStopChanged += new EventHandler(CorrigirTabStop);
                cbxEstadoCivil.Controls.Add(rb);
                iRB++;
            }

        }
    
        private EnumEstadoCivil? LerEstadoCivil()
        {
            foreach(var control in cbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if (rb.Checked)
                {
                    return (EnumEstadoCivil)(rb.Tag);
                }
            }
            return null;
        }
      
        private void MarcarEstadoCivil(EnumEstadoCivil estadoCivil)
        {
            foreach (var control in cbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if ((EnumEstadoCivil)(rb.Tag) == estadoCivil) 
                 rb.Checked = true;
                return;
            }
        }

        private void MarcaEstadoCivil(EnumEstadoCivil estadoCivil)
        {
            foreach ( var control in cbxEstadoCivil.Controls)
            {
                RadioButton rb = control as RadioButton;
                if ((EnumEstadoCivil) (rb.Tag) == estadoCivil)
                {
                    rb.Checked = true;
                    return;
                }
            }
        }
     
        private void LimparDados()
        {
            txtCodigo.Clear();
            txtNome.Clear();
            mtbCPF.Clear();
            dtpDataNascimento.Value = DateTime.Now.Date;
            nudRendaMensal.Value = 0;
            foreach (var control in cbxEstadoCivil.Controls)
            {
                (control as RadioButton).Checked = false;
            }
            cbxNacionalidade.SelectedIndex = -1;
            mtbPlacaVeiculo.Clear();
            chxTemFilhos.CheckState = CheckState.Indeterminate;
        }

        private void PreencherCamposComCliente(Cliente cliente)
        {
           txtCodigo.Text = cliente.Codigo.ToString();
           txtNome.Text = cliente.Nome.ToString();
           mtbCPF.Text = cliente.CPF.ToString();
           dtpDataNascimento.Value = cliente.DataNascimento;
           nudRendaMensal.Value = cliente.RendaMensal;
           MarcarEstadoCivil(cliente.EstadoCivil);
           cbxNacionalidade.SelectedValue = cliente.Nacionalidade;
           mtbPlacaVeiculo.Text = cliente.PlacaVeiculo;
           chxTemFilhos.Checked = cliente.TemFilhos;
        }

        private void PreencherClienteComCampos(Cliente cliente)
        {
            cliente.Nome = txtNome.Text;
            cliente.CPF = (int)Convert.ToInt64(mtbCPF.Text);
            cliente.DataNascimento = dtpDataNascimento.Value.Date;
            cliente.RendaMensal = nudRendaMensal.Value;
            cliente.EstadoCivil = LerEstadoCivil().Value;
            cliente.TemFilhos = chxTemFilhos.Checked;
            cliente.Nacionalidade = cbxNacionalidade.SelectedValue.ToString();
            cliente.PlacaVeiculo = mtbPlacaVeiculo.Text;
        }
    
        private bool PossuiValoresNaoSalvos()
        {
            if (cbxCliente.SelectedIndex < 0)
            {
                if(!String.IsNullOrWhiteSpace(txtNome.Text)) return true;
                if(!String.IsNullOrWhiteSpace(mtbCPF.Text)) return true;
                if(dtpDataNascimento.Value.Date != DateTime.Now.Date) return true;
                if(nudRendaMensal.Value > 0) return true;
                if(LerEstadoCivil() != null) return true;
                if (cbxNacionalidade.SelectedIndex >= 0) return true;
                if(chxTemFilhos.CheckState != CheckState.Indeterminate) return true;
            }
            else
            {
                Cliente cliente = cbxCliente.SelectedItem as Cliente;
                if (txtNome.Text.Trim() != cliente.Nome) return true;
                if (mtbCPF.Text != cliente.CPF.ToString()) return true;
                if(dtpDataNascimento.Value.Date != cliente.DataNascimento) return true;
                if(nudRendaMensal.Value != cliente.RendaMensal) return true;
                if(LerEstadoCivil() != cliente.EstadoCivil) return true;
                if(cbxNacionalidade.SelectedValue.ToString() != cliente.Nacionalidade) return true;
                if(mtbPlacaVeiculo.Text != cliente.PlacaVeiculo) return true;
                if(chxTemFilhos.Checked != cliente.TemFilhos) return true;
        
            }
             return false;
        }
     
        private void AlterarEstadosDosCampos(bool estado)
        {
            txtNome.Enabled = estado;
            mtbCPF.Enabled = estado;
            dtpDataNascimento.Enabled = estado;
            nudRendaMensal.Enabled = estado;
            cbxEstadoCivil.Enabled = estado;
            cbxNacionalidade.Enabled = estado;
            mtbPlacaVeiculo.Enabled = estado;
            chxTemFilhos.Enabled = estado;  
            btnSalvar.Enabled = estado;
            btnSalvar.Enabled = estado; 

        }
       
        private void HabilitarCampos()
        {
            AlterarEstadosDosCampos(true);
        }
        
        private void DesabilitarCampos()
        {
            AlterarEstadosDosCampos(false);
        }
       
        private bool PrencheuTodosOsCampos()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text)) return false;
            if (String.IsNullOrWhiteSpace(mtbCPF.Text)) return false;
            if (dtpDataNascimento.Value.Date == DateTime.Now.Date) return false;
            if (nudRendaMensal.Value == 0) return false;
            if (LerEstadoCivil() == null) return false;
            if (cbxNacionalidade.SelectedIndex < 0) return false;
            if (String.IsNullOrWhiteSpace(mtbPlacaVeiculo.Text)) return false;
            if (chxTemFilhos.CheckState == CheckState.Indeterminate) return false;

            return true;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            cbxCliente.SelectedIndex = -1;
            LimparDados();
            HabilitarCampos();
            txtNome.Select();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (PrencheuTodosOsCampos())
            {
                Cliente cliente = cbxCliente.SelectedIndex < 0 ?
                    new Cliente() : cbxCliente.SelectedItem as Cliente;

                PreencherClienteComCampos(cliente);
                DesabilitarCampos();

                if (cbxCliente.SelectedIndex < 0)
                {
                    cliente = Cliente.Inserir(cliente);
                    AtualizarComboBoxClientes();
                    Infomar("Cliente cadastrado com sucesso!");
                }
                else
                {
                    AtualizarComboBoxClientes();
                    Infomar("Cliente alterado com sucesso!");
                }
            }
            else
            {
                Infomar("Só é possivel  salvar se todos os campos forem preenchidos. Salvamento ");
            }
        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (PossuiValoresNaoSalvos())
            {
                if (Confirmar("Há alterações não salvas. Deseja realmente cancelar?"))
                {
                    cbxCliente.SelectedIndex = -1;
                    LimparCampos();
                    cbxCliente.Select();
                    DesabilitarCampos();
                }
            }
            else
            {
                cbxCliente.SelectedIndex -= 1;
                LimparCampos();
                cbxCliente.Select();
                DesabilitarCampos();

            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }


     
    
}
