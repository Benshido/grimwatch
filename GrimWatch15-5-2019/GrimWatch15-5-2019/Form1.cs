using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace GrimWatch15_5_2019
{
    public partial class frmGrimWatchBvri : Form
    {
        //For the background music
        System.Media.SoundPlayer backgroundMusicBvri = new System.Media.SoundPlayer();

        //all boolean variables
        bool loadSwitchSvog = false;
        bool adventureModeBvri = false;
        bool hellrushModeBvri = false;
        bool playerTurnBvri = false;
        bool playerKnightBvri = false;
        bool playerArcherBvri = false;
        bool defenseBvri = false;
        bool missedAttackBvri = false;

        //all string variables
        string chosenHeroBvri = "";
        string soundFileBvri = "";
        string backgroundSoundBvri = "";

        //All int variables
        //true damage is all the damage including stats damage and minus defense from enemy
        int trueDamageBvri = 0;
        int randomDamageBvri = 0;
        int bossDefeatedBvri = 0;
        int potionAmountBvri = 3;
        int potionAmountLeftBvri = 0;
        int characterDamageStatBvri = 0;
        int characterDefenseStatBvri = 0;
        int characterHealthStatBvri = 0;
        int chosenAttackBvri = 0;
        int usedPotionBvri = 0;
        int extraBossDamageBvri = 0;
        int randomHitChanceIntegerBvri = 0;

        //Boss int variables
        int randomBossBvri = 0;
        int randomBossAttackBvri = 0;
        int randomBossDamageBvri = 0;
        int bossAttackOneBvri = 0;
        int bossAttackTwoBvri = 0;
        int bossAttackThreeBvri = 0;
        
        //All random variables and lists
        Random randomAttackGeneratorBvri = new Random();
        Random randomDamageGeneratorBvri = new Random();
        Random randomBossGeneratorBvri = new Random();
        Random randomHitChanceBvri = new Random();
        List<CharacterListBvri> CharactersBvri = new List<CharacterListBvri>();
        List<AttackListBvri> AttacksBvri = new List<AttackListBvri>();

        #region audio variabelen
        [DllImport("winmm.dll")]
        //default audio handling code
        private static extern long mciSendString(
        string strCommand,
        StringBuilder strReturn,
        int iReturnLength,
        IntPtr hwndCallback);
        #endregion

        public frmGrimWatchBvri()
        {
            InitializeComponent();
        }

        private void frmGrimWatchBvri_Load(object sender, EventArgs e)
        {
            //Database connection and query
            #region dbConnect
            //database connection
            string connectionStringSvog;
            connectionStringSvog = "server=localhost;port=3306;uid=Proftaak;pwd=Kaas;database=grimwatch;SslMode=none;";
            MySqlConnection connectionSvog = new MySqlConnection(connectionStringSvog);
            connectionSvog.Open();
            #endregion
            #region Query
            MySqlDataAdapter sqlQuerySvog = new MySqlDataAdapter("SELECT * FROM characters", connectionSvog);
            DataTable dataTableSvog = new DataTable();
            sqlQuerySvog.Fill(dataTableSvog);
            foreach (DataRow Row in dataTableSvog.Rows)
            {
                CharactersBvri.Add(new CharacterListBvri(Row["charId"].ToString(),
                                                         Row["charName"].ToString(),
                                                         Row["charHealth"].ToString(),
                                                         Row["charDamage"].ToString(),
                                                         Row["charDefense"].ToString(),
                                                         Row["charImage"].ToString()));
            }
            connectionSvog.Close();
            

            MySqlDataAdapter sqlAttackQuerySvog = new MySqlDataAdapter("SELECT * FROM attack", connectionSvog);
            DataTable dataTableAttacksSvog = new DataTable();
            sqlAttackQuerySvog.Fill(dataTableAttacksSvog);
            foreach (DataRow Row in dataTableAttacksSvog.Rows)
            {
                AttacksBvri.Add(new AttackListBvri(Row["attackName"].ToString(),
                                                   Row["charId"].ToString(),
                                                   Row["minAttackDamage"].ToString(),
                                                   Row["maxAttackDamage"].ToString()));
            }
            #endregion
            //Hide the tabs from the tabcontrol
            tbcTabScreensBvri.Appearance = TabAppearance.FlatButtons;
            tbcTabScreensBvri.ItemSize = new Size(0, 1);
            tbcTabScreensBvri.SizeMode = TabSizeMode.Fixed;
            pgbLoadSvog.Style = ProgressBarStyle.Continuous;
            tbcTabScreensBvri.SelectedIndex = 2;
            tmrLoadSvog.Enabled = true;
            pgbHeroHealthBvri.Maximum = 250;

            backgroundMusicBvri.SoundLocation = Application.StartupPath
                                    + "\\..\\..\\sounds\\background.wav";

            //Start up loading screen
            if (pgbLoadSvog.Value == 100)
            {
                tmrLoadSvog.Enabled = false;
                tbcTabScreensBvri.SelectedIndex = 0;
                loadSwitchSvog = true;
            }
        }

        private void pcbAdventureButtonBvri_Click(object sender, EventArgs e)
        {
            //turns on adventure mode for later
            adventureModeBvri = true;
            tbcTabScreensBvri.SelectedIndex = 1;
        }

        private void pcbExitBvri_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pcbChooseKnightSvog_MouseHover(object sender, EventArgs e)
        {
            this.pcbChooseCharacterBvri.Load(Application.StartupPath + "\\..\\..\\art\\knight.gif");
        }

        private void pcbChooseArcherSvog_MouseHover(object sender, EventArgs e)
        {
            this.pcbChooseCharacterBvri.Load(Application.StartupPath + "\\..\\..\\art\\archer.gif");
        }

        private void pcbChooseKnightSvog_Click(object sender, EventArgs e)
        {
            //Choose knight
            chosenHeroBvri = "knight";
            playerKnightBvri = true;
            lblHeroNameSvog.Text = "Knight";
            ChooseCharacterBvri();
            characterDamageStatBvri = Convert.ToInt32(CharactersBvri[0].CharacterDamageBvri);
            characterDefenseStatBvri = Convert.ToInt32(CharactersBvri[0].CharacterDefenseBvri);
            this.pcbAttackOneSvog.Load(Application.StartupPath + "\\..\\..\\art\\swordstrike.jpg");
            this.pcbAttackTwoSvog.Load(Application.StartupPath + "\\..\\..\\art\\ussr.jpg");
            this.pcbAttackThreeSvog.Load(Application.StartupPath + "\\..\\..\\art\\holyhell.jpg");
        }

        private void pcbChooseArcherSvog_Click(object sender, EventArgs e)
        {
            //Choose knight
            chosenHeroBvri = "archer";
            playerArcherBvri = true;
            lblHeroNameSvog.Text = "Archer";
            ChooseCharacterBvri();
            characterDamageStatBvri = Convert.ToInt32(CharactersBvri[1].CharacterDamageBvri);
            characterDefenseStatBvri = Convert.ToInt32(CharactersBvri[1].CharacterDefenseBvri);
            this.pcbAttackOneSvog.Load(Application.StartupPath + "\\..\\..\\art\\piercingarrow.jpg");
            this.pcbAttackTwoSvog.Load(Application.StartupPath + "\\..\\..\\art\\arrowrain.jpg");
            this.pcbAttackThreeSvog.Load(Application.StartupPath + "\\..\\..\\art\\arrowdynamicstrike.jpg");
        }

        private void tmrLoadSvog_Tick(object sender, EventArgs e)
        {
            //loading screen logic 
            pgbLoadSvog.Value = pgbLoadSvog.Value + 25;
            if (pgbLoadSvog.Value == 100 )
            {      
                //turns off timer   
                tmrLoadSvog.Enabled = false;
                pgbLoadSvog.Value = 0;
                //this code makes sure the user ends up on the correct screen 
                if (loadSwitchSvog == true)
                {
                    tbcTabScreensBvri.SelectedIndex = 3;                    
                }
                else
                {
                    tbcTabScreensBvri.SelectedIndex = 0;
                    
                    loadSwitchSvog = true;
                }
            }
        }

        private void pcbBackToMenuSvog_Click(object sender, EventArgs e)
        {
            //Goes back to the main menu
            tbcTabScreensBvri.SelectedIndex = 0;
            adventureModeBvri = false;
            hellrushModeBvri = false;
            playerTurnBvri = false;
            playerKnightBvri = false;
            playerArcherBvri = false;
            rtbBattleTextBvri.Clear();
            this.pcbWeaponAllyBvri.Image = null;
            this.pcbWeaponEnemyBvri.Image = null;
            backgroundMusicBvri.Stop();
        }

        private void pcbCreditsBvri_Click(object sender, EventArgs e)
        {
            tbcTabScreensBvri.SelectedIndex = 4;
        }

        private void pcbHellrushBvri_Click(object sender, EventArgs e)
        {
            hellrushModeBvri = true;
            tbcTabScreensBvri.SelectedIndex = 1;
        }

        //Method for choosing character
        private void ChooseCharacterBvri()
        {
            tbcTabScreensBvri.SelectedIndex = 2;
            tmrLoadSvog.Enabled = true;
            this.pcbHeroSvog.Load(Application.StartupPath + "\\..\\..\\art\\" + chosenHeroBvri + ".gif");
            tbcTabScreensBvri.TabPages[3].BackgroundImage = Image.FromFile(Application.StartupPath
                                                + "\\..\\..\\art\\mountain.jpg");
            this.pcbWeaponAllyBvri.Image = null;
            this.pcbWeaponEnemyBvri.Image = null;

            //play background music
            backgroundMusicBvri.Play();

            //Battle stats
            playerTurnBvri = true;
            potionAmountLeftBvri = potionAmountBvri;
            lblPotionAmountSvog.Text = Convert.ToString(potionAmountLeftBvri);
            characterHealthStatBvri = Convert.ToInt32(CharactersBvri[0].CharacterHealthBvri);
            pgbHeroHealthBvri.Maximum = characterHealthStatBvri;
            pgbHeroHealthBvri.Value = characterHealthStatBvri;
            potionAmountBvri = 3;
            bossDefeatedBvri = 0;
            lblBossDefeatedBvri.Text = Convert.ToString(bossDefeatedBvri);
            extraBossDamageBvri = 0;
            pcbAttackOneSvog.Show();
            pcbAttackTwoSvog.Show();
            pcbAttackThreeSvog.Show();
            pcbPotionSvog.Show();
            pcbDefenseSvog.Show();
            pcbNextBvri.Hide();

            //Random boss generated and stats
            randomBossBvri = randomBossGeneratorBvri.Next(2, 11);
            this.pcbLoadingScreenSvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
            this.pcbEnemySvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
            lblEnemyNameBvri.Text = CharactersBvri[randomBossBvri].CharacterNameBvri;
            pgbEnemyHealthSvog.Maximum = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
            pgbEnemyHealthSvog.Value = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
        }

        #region battle sequence
        //Battle Sequence
        private void pcbPotionSvog_Click(object sender, EventArgs e)
        {
            rtbBattleTextBvri.Clear();
            if(potionAmountLeftBvri != 0)
            {
                //Potion add 100 HP to your health bar
                soundFileBvri = "potion";
                PlayAudioBvri();
                usedPotionBvri = pgbHeroHealthBvri.Value + 125;
                if (usedPotionBvri >= pgbHeroHealthBvri.Maximum)
                {
                    pgbHeroHealthBvri.Value = pgbHeroHealthBvri.Maximum;
                }
                else
                {
                    pgbHeroHealthBvri.Value += 125;
                }
                potionAmountLeftBvri -= 1;
                lblPotionAmountSvog.Text = Convert.ToString(potionAmountLeftBvri);
                pcbAttackOneSvog.Hide();
                pcbAttackTwoSvog.Hide();
                pcbAttackThreeSvog.Hide();
                pcbPotionSvog.Hide();
                pcbDefenseSvog.Hide();
                lblPotionAmountSvog.Hide();
                pcbNextBvri.Show();
                rtbBattleTextBvri.AppendText("You used a Potion");
                playerTurnBvri = false;

            }
            else
            {
                rtbBattleTextBvri.AppendText("You have no potions left!");
            }
        }

        private void pcbDefenseSvog_Click(object sender, EventArgs e)
        {
            //Defense
            soundFileBvri = "shield";
            PlayAudioBvri();
            rtbBattleTextBvri.Clear();
            defenseBvri = true;
            this.pcbWeaponAllyBvri.Load(Application.StartupPath + "\\..\\..\\art\\shield.gif");
            pcbAttackOneSvog.Hide();
            pcbAttackTwoSvog.Hide();
            pcbAttackThreeSvog.Hide();
            pcbPotionSvog.Hide();
            pcbDefenseSvog.Hide();
            lblPotionAmountSvog.Hide();
            pcbNextBvri.Show();
            rtbBattleTextBvri.AppendText("You used Defense");
            playerTurnBvri = false;
        }

        private void pcbAttackOneSvog_Click(object sender, EventArgs e)
        {
            rtbBattleTextBvri.Clear();
            randomHitChanceIntegerBvri = randomHitChanceBvri.Next(0, 100);
            if (randomHitChanceIntegerBvri > 100)
            {
                missedAttackBvri = true;
            }
            else if (randomHitChanceIntegerBvri <= 100)
            {
                missedAttackBvri = false;
            }
            //Attack one
            this.pcbWeaponAllyBvri.Load(Application.StartupPath + "\\..\\..\\art\\swordally.gif");
            if (playerKnightBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 0;
            }
            else if (playerArcherBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 3;
            }
            ChosenAttackBvri();
        }

        private void pcbAttackTwoSvog_Click(object sender, EventArgs e)
        {
            rtbBattleTextBvri.Clear();
            randomHitChanceIntegerBvri = randomHitChanceBvri.Next(0, 100);
            if(randomHitChanceIntegerBvri > 80)
            {
                missedAttackBvri = true;
            }
            else if (randomHitChanceIntegerBvri <= 80)
            {
                missedAttackBvri = false;
            }

            //Attack two
            if (playerKnightBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 1;
            }
            else if (playerArcherBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 4;
            }
            ChosenAttackBvri();
        }

        private void pcbAttackThreeSvog_Click(object sender, EventArgs e)
        {
            rtbBattleTextBvri.Clear();
            randomHitChanceIntegerBvri = randomHitChanceBvri.Next(0, 100);
            if (randomHitChanceIntegerBvri > 60)
            {
                missedAttackBvri = true;
            }
            else if (randomHitChanceIntegerBvri <= 60)
            {
                missedAttackBvri = false;
            }
            //Attack three
            if (playerKnightBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 2;
            }
            else if (playerArcherBvri == true && playerTurnBvri == true)
            {
                chosenAttackBvri = 5;
            }
            ChosenAttackBvri();
        }

        //Method for chosen attack
        private void ChosenAttackBvri()
        {
            if (playerTurnBvri == true)
            {
                soundFileBvri = "sword";
                PlayAudioBvri();
                this.pcbWeaponAllyBvri.Load(Application.StartupPath + "\\..\\..\\art\\swordally.gif");
                pcbAttackOneSvog.Hide();
                pcbAttackTwoSvog.Hide();
                pcbAttackThreeSvog.Hide();
                pcbPotionSvog.Hide();
                pcbDefenseSvog.Hide();
                lblPotionAmountSvog.Hide();
                if(missedAttackBvri == true)
                {
                    trueDamageBvri = 0;
                    rtbBattleTextBvri.AppendText("You missed!");
                }
                else
                {
                    randomDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[chosenAttackBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[chosenAttackBvri].MaxAttackDamageBvri));
                    trueDamageBvri = randomDamageBvri + characterDamageStatBvri - Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDefenseBvri);
                    rtbBattleTextBvri.AppendText("You used " + AttacksBvri[chosenAttackBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                }

                if (pgbEnemyHealthSvog.Value <= trueDamageBvri)
                {
                    pcbNextBvri.Show();
                    pgbEnemyHealthSvog.Value = 0;
                    backgroundMusicBvri.Stop();
                    rtbBattleTextBvri.AppendText("\n" + CharactersBvri[randomBossBvri].CharacterNameBvri + " has been defeated! Click Next to continue!");
                }
                else
                {
                    pgbEnemyHealthSvog.Value -= trueDamageBvri;
                }
                playerTurnBvri = false;
                missedAttackBvri = false;
                pcbNextBvri.Show();
            }
        }

        private void pcbNextBvri_Click(object sender, EventArgs e)
        {
            StopAudioBvri();
            //Checks if the no one has 0 health
            if (pgbHeroHealthBvri.Value == 0)
            {
                if (playerArcherBvri == true)
                {
                    soundFileBvri = "archerdeath";
                }
                else if (playerKnightBvri == true)
                {
                    soundFileBvri = "knightdeath";
                }
                backgroundMusicBvri.Stop();
                PlayAudioBvri();
                rtbBattleTextBvri.Clear();
                tbcTabScreensBvri.SelectedIndex = 6;
                lblFinishGameOverBvri.Text = "Game Over";
                missedAttackBvri = false;
                lblHighScoreBvri.Text = Convert.ToString(bossDefeatedBvri);
            }
            else if (pgbEnemyHealthSvog.Value == 0)
            {
                backgroundMusicBvri.Stop();
                soundFileBvri = "enemydeath";
                PlayAudioBvri();
                rtbBattleTextBvri.Clear();
                tbcTabScreensBvri.SelectedIndex = 5;
                characterHealthStatBvri += 10;
                pgbHeroHealthBvri.Maximum = characterHealthStatBvri;
                pgbHeroHealthBvri.Value = characterHealthStatBvri;
                characterDamageStatBvri += 5;
                characterDefenseStatBvri += 5;
                potionAmountLeftBvri = potionAmountBvri;
                lblPotionAmountSvog.Text = Convert.ToString(potionAmountLeftBvri);
                lblNewDamageStatBvri.Text = Convert.ToString(characterDamageStatBvri);
                lblNewDefenseStatBvri.Text = Convert.ToString(characterDefenseStatBvri);
                lblNewHealthStatBvri.Text = Convert.ToString(characterHealthStatBvri);
                bossDefeatedBvri += 1;
                lblBossDefeatedBvri.Text = Convert.ToString(bossDefeatedBvri);
                lblHighScoreBvri.Text = Convert.ToString(bossDefeatedBvri);
                missedAttackBvri = false;
            }

            if (playerTurnBvri == true && (pgbHeroHealthBvri.Value != 0 || pgbEnemyHealthSvog.Value != 0))
            {
                pcbAttackOneSvog.Show();
                pcbAttackTwoSvog.Show();
                pcbAttackThreeSvog.Show();
                pcbPotionSvog.Show();
                pcbDefenseSvog.Show();
                lblPotionAmountSvog.Show();
                rtbBattleTextBvri.Clear();
                this.pcbWeaponEnemyBvri.Image = null;
                this.pcbWeaponAllyBvri.Image = null;
                missedAttackBvri = false;
                defenseBvri = false;
            }

            if (defenseBvri == false)
            {
                this.pcbWeaponAllyBvri.Image = null;
            }

            if (playerTurnBvri == false && (pgbHeroHealthBvri.Value != 0 || pgbEnemyHealthSvog.Value != 0))
            {
                soundFileBvri = "sword";
                PlayAudioBvri();
                rtbBattleTextBvri.Clear();
                playerTurnBvri = true;
                pcbNextBvri.Hide();
                //All attacks from all bosses
                randomBossAttackBvri = randomAttackGeneratorBvri.Next(0, 100);
                #region All attacks from all bosses
                if (randomBossBvri == 2)
                {
                    bossAttackOneBvri = 6;
                    bossAttackTwoBvri = 7;
                    bossAttackThreeBvri = 8;
                }
                else if (randomBossBvri == 3)
                {
                    bossAttackOneBvri = 9;
                    bossAttackTwoBvri = 10;
                    bossAttackThreeBvri = 11;
                }
                else if (randomBossBvri == 4)
                {
                    bossAttackOneBvri = 12;
                    bossAttackTwoBvri = 13;
                    bossAttackThreeBvri = 14;
                }
                else if (randomBossBvri == 5)
                {
                    bossAttackOneBvri = 15;
                    bossAttackTwoBvri = 16;
                    bossAttackThreeBvri = 17;
                }
                else if (randomBossBvri == 6)
                {
                    bossAttackOneBvri = 18;
                    bossAttackTwoBvri = 19;
                    bossAttackThreeBvri = 20;
                }
                else if (randomBossBvri == 7)
                {
                    bossAttackOneBvri = 21;
                    bossAttackTwoBvri = 22;
                    bossAttackThreeBvri = 23;
                }
                else if (randomBossBvri == 8)
                {
                    bossAttackOneBvri = 24;
                    bossAttackTwoBvri = 25;
                    bossAttackThreeBvri = 26;
                }
                else if (randomBossBvri == 9)
                {
                    bossAttackOneBvri = 27;
                    bossAttackTwoBvri = 28;
                    bossAttackThreeBvri = 29;
                }
                else if (randomBossBvri == 10)
                {
                    bossAttackOneBvri = 30;
                    bossAttackTwoBvri = 31;
                    bossAttackThreeBvri = 32;
                }
                else if (randomBossBvri == 11)
                {
                    bossAttackOneBvri = 33;
                    bossAttackTwoBvri = 34;
                    bossAttackThreeBvri = 35;
                }
                #endregion
                randomHitChanceIntegerBvri = randomHitChanceBvri.Next(0, 100);
                if (randomHitChanceIntegerBvri > 70)
                {
                    missedAttackBvri = true;
                }
                else if (randomHitChanceIntegerBvri <= 70)
                {
                    missedAttackBvri = false;
                }

                //Damage with and without defense
                if (missedAttackBvri == false)
                {
                    if (defenseBvri == true)
                    {
                        if (randomBossAttackBvri >= 0 && randomBossAttackBvri <= 45)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackOneBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackOneBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri) - characterDefenseStatBvri;
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackOneBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                        else if (randomBossAttackBvri > 45 && randomBossAttackBvri <= 90)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackTwoBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackTwoBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri) - characterDefenseStatBvri;
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackTwoBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                        else if (randomBossAttackBvri > 90 && randomBossAttackBvri <= 100)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackThreeBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackThreeBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri) - characterDefenseStatBvri;
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackThreeBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                    }
                    else
                    {
                        if (randomBossAttackBvri >= 0 && randomBossAttackBvri <= 45)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackOneBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackOneBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri);
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackOneBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                        else if (randomBossAttackBvri > 45 && randomBossAttackBvri <= 90)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackTwoBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackTwoBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri);
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackTwoBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                        else if (randomBossAttackBvri > 90 && randomBossAttackBvri <= 100)
                        {
                            randomBossDamageBvri = randomDamageGeneratorBvri.Next(Convert.ToInt32(AttacksBvri[bossAttackThreeBvri].MinAttackDamageBvri), Convert.ToInt32(AttacksBvri[bossAttackThreeBvri].MaxAttackDamageBvri));
                            trueDamageBvri = extraBossDamageBvri + randomBossDamageBvri + Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterDamageBvri);
                            rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " used " + AttacksBvri[bossAttackThreeBvri].AttackNameBvri + ", it dealt " + trueDamageBvri + " damage!");
                        }
                    }
                }
                else
                {
                    trueDamageBvri = 0;
                    rtbBattleTextBvri.AppendText(CharactersBvri[randomBossBvri].CharacterNameBvri + " missed!");
                }

                this.pcbWeaponEnemyBvri.Load(Application.StartupPath + "\\..\\..\\art\\sword.gif");
                if (pgbHeroHealthBvri.Value <= trueDamageBvri)
                {
                    pcbNextBvri.Show();
                    pgbHeroHealthBvri.Value = 0;
                    rtbBattleTextBvri.AppendText("\n You have been defeated! Click Next to continue!");
                }
                else
                {
                    if (trueDamageBvri < 0 || trueDamageBvri == 0)
                    {
                        pgbHeroHealthBvri.Value = pgbHeroHealthBvri.Value;
                    }
                    else
                    {
                        pgbHeroHealthBvri.Value -= trueDamageBvri;
                    }
                }
                playerTurnBvri = true;
                pcbNextBvri.Show();
            }
        }
        #endregion

        private void pcbNextBattleBvri_Click(object sender, EventArgs e)
        {
            rtbBattleTextBvri.Clear();
            this.pcbWeaponAllyBvri.Image = null;
            this.pcbWeaponEnemyBvri.Image = null;
            //Changes background after 5 bosses
            if (bossDefeatedBvri == 5)
            {
                tbcTabScreensBvri.TabPages[3].BackgroundImage = Image.FromFile(Application.StartupPath
                                    + "\\..\\..\\art\\dungeon.jpg");
            }

            if (bossDefeatedBvri != 10)
            {
                //Show and Hide buttons
                pcbAttackOneSvog.Show();
                pcbAttackTwoSvog.Show();
                pcbAttackThreeSvog.Show();
                pcbPotionSvog.Show();
                pcbDefenseSvog.Show();
                pcbNextBvri.Hide();

                backgroundMusicBvri.Play();

                //Player stats
                playerTurnBvri = true;
                potionAmountLeftBvri = potionAmountBvri;
                lblPotionAmountSvog.Text = Convert.ToString(potionAmountLeftBvri);
                pgbHeroHealthBvri.Maximum = Convert.ToInt32(CharactersBvri[0].CharacterHealthBvri);
                pgbHeroHealthBvri.Value = Convert.ToInt32(CharactersBvri[0].CharacterHealthBvri);

                //Loading Screen
                tbcTabScreensBvri.SelectedIndex = 2;
                tmrLoadSvog.Enabled = true;

                //Boss stats
                randomBossBvri = randomBossGeneratorBvri.Next(2, 11);
                this.pcbLoadingScreenSvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
                this.pcbEnemySvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
                lblEnemyNameBvri.Text = CharactersBvri[randomBossBvri].CharacterNameBvri;
                pgbEnemyHealthSvog.Maximum = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
                pgbEnemyHealthSvog.Value = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
            }
            else
            {
                if(adventureModeBvri == true)
                {
                    tbcTabScreensBvri.SelectedIndex = 6;
                    lblFinishGameOverBvri.Text = "Congratulations!";
                    adventureModeBvri = false;
                    hellrushModeBvri = false;
                    playerTurnBvri = false;
                    playerKnightBvri = false;
                    playerArcherBvri = false;
                    rtbBattleTextBvri.Clear();
                    this.pcbWeaponAllyBvri.Image = null;
                    this.pcbWeaponEnemyBvri.Image = null;
                }
                //buff enemy every 10 bosses
                else if (hellrushModeBvri == true)
                {
                    if(bossDefeatedBvri == 10 || bossDefeatedBvri == 20 || bossDefeatedBvri == 30 || bossDefeatedBvri == 40 ||
                       bossDefeatedBvri == 50 || bossDefeatedBvri == 60 || bossDefeatedBvri == 70 || bossDefeatedBvri == 80 ||
                       bossDefeatedBvri == 90 || bossDefeatedBvri == 100)
                    {
                        extraBossDamageBvri += 10;
                    }
                    //Show and Hide buttons
                    pcbAttackOneSvog.Show();
                    pcbAttackTwoSvog.Show();
                    pcbAttackThreeSvog.Show();
                    pcbPotionSvog.Show();
                    pcbDefenseSvog.Show();
                    pcbNextBvri.Hide();

                    backgroundMusicBvri.Play();

                    //Player stats
                    playerTurnBvri = true;
                    potionAmountLeftBvri = potionAmountBvri;
                    lblPotionAmountSvog.Text = Convert.ToString(potionAmountLeftBvri);
                    pgbHeroHealthBvri.Maximum = Convert.ToInt32(CharactersBvri[0].CharacterHealthBvri);
                    pgbHeroHealthBvri.Value = Convert.ToInt32(CharactersBvri[0].CharacterHealthBvri);

                    //Loading Screen
                    tbcTabScreensBvri.SelectedIndex = 2;
                    tmrLoadSvog.Enabled = true;

                    //Boss stats
                    randomBossBvri = randomBossGeneratorBvri.Next(2, 11);
                    this.pcbLoadingScreenSvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
                    this.pcbEnemySvog.Load(Application.StartupPath + "\\..\\..\\art\\" + CharactersBvri[randomBossBvri].CharacterImageBvri + ".gif");
                    lblEnemyNameBvri.Text = CharactersBvri[randomBossBvri].CharacterNameBvri;
                    pgbEnemyHealthSvog.Maximum = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
                    pgbEnemyHealthSvog.Value = Convert.ToInt32(CharactersBvri[randomBossBvri].CharacterHealthBvri);
                }
            }
        }

        private void pcbNextFinishGameOverBvri_Click(object sender, EventArgs e)
        {
            //Goes back to the main menu
            if (bossDefeatedBvri != 10)
            {
                tbcTabScreensBvri.SelectedIndex = 0;
                adventureModeBvri = false;
                hellrushModeBvri = false;
                playerTurnBvri = false;
                playerKnightBvri = false;
                playerArcherBvri = false;
                rtbBattleTextBvri.Clear();
            }
            else
            {
                tbcTabScreensBvri.SelectedIndex = 4;
                adventureModeBvri = false;
                hellrushModeBvri = false;
                playerTurnBvri = false;
                playerKnightBvri = false;
                playerArcherBvri = false;
                rtbBattleTextBvri.Clear();
            }
        }

        #region audio
        private void PlayAudioBvri()
        {
            //Method to play a sound
            //Searches the located file and plays it
            mciSendString("open \"" + Application.StartupPath
                  + "\\..\\..\\sounds\\"
                  + soundFileBvri + ".mp3\" "
                  + "type mpegvideo alias MediaFile",
           null, 0, IntPtr.Zero);
            mciSendString("play MediaFile", null, 0, IntPtr.Zero);
        }

        private static void StopAudioBvri()
        {
            //Method to stop the audio
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
        }
        #endregion
    }
}
