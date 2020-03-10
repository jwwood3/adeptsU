using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameMaster;

public class PhaseIndicator : MonoBehaviour
{
    TextMeshProUGUI indic;
    // Start is called before the first frame update
    void Start()
    {
        indic = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSpell == "")
        {
            switch (phase)
            {
                case 1:
                    switch (subPhase)
                    {
                        case 1:
                            indic.text = "Phase 1-1: Spell Casting";
                            break;
                        case 2:
                            indic.text = "Phase 1-2: Topaz Adept Miners";
                            break;
                        case 3:
                            indic.text = "Phase 1-3: Miner Placement";
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    switch (subPhase)
                    {
                        case 1:
                            indic.text = "Phase 2-1: Spell Casting";
                            break;
                        case 2:
                            indic.text = "Phase 2-2: Emerald Adept Mine Selection";
                            break;
                        case 3:
                            indic.text = "Phase 3-3: Miner Production";
                            break;
                        default:
                            break;
                    }
                    break;
                case 3:
                    switch (subPhase)
                    {
                        case 1:
                            indic.text = "Phase 3-1: Spell Casting";
                            break;
                        case 2:
                            indic.text = "Phase 3-2: Spell Selection";
                            break;
                        default:
                            break;
                    }
                    break;
                case 4:
                    indic.text = "Phase 4: Adept Bonuses";
                    break;
                case 5:
                    switch (subPhase)
                    {
                        case 1:
                            indic.text = "Phase 5-1: Spell Casting";
                            break;
                        case 2:
                            indic.text = "Phase 5-2: Onyx Adept Battery";
                            break;
                        case 3:
                            indic.text = "Phase 5-3: Diamond Adept Gem Selection";
                            break;
                        case 4:
                            indic.text = "Phase 5-4: Battery Level Advancement";
                            break;
                        case 5:
                            indic.text = "Phase 5-5: Gem Processing";
                            break;
                        default:
                            break;
                    }
                    break;
            }
            if (checking)
            {
                indic.text = "Too many cards: Discard";
            }
        }
        else if (activeSpell == "Ambition2" || activeSpell == "Ambition1")
        {
            indic.text = "Ambition: Pick a mine to place a miner";
        }
        else if (activeSpell == "Initiative")
        {
            indic.text = "Initiative: Select a mine to place your miners";
        }
        else if (activeSpell == "Backlash")
        {
            indic.text = "Backlash: Select an appropriate mine to backlash";
        }
        else if (activeSpell == "Barrier")
        {
            indic.text = "Barrier: Choose a mine to close";
        }
        else if (activeSpell == "Gateways")
        {
            indic.text = "Gateways: Take an extra turn to place your miners";
        }
        else if (activeSpell == "Precedence")
        {
            indic.text = "Precedence: Choose mines to place all of your miners";
        }
        else if (activeSpell == "Telepathy" && players[turn].miners == 0)
        {
            indic.text = "Telepathy: Choose a mine or worksite to reclaim your miners";
        }
        else if (activeSpell == "Telepathy")
        {
            indic.text = "Telepathy: Choose which mines or work sites to replace your miners";
        }
        else if (activeSpell == "Teleport")
        {
            indic.text = "Teleport: Choose a mine or worksite to remove enemy miners from";
        }
        else if (activeSpell == "Extraction2" || activeSpell == "Extraction1")
        {
            indic.text = "Extraction: Choose a mine or worksite to rush a miner's production";
        }
        else if (activeSpell == "Telekinesis")
        {
            indic.text = "Telekinesis: Choose a mine to take gems from";
        }
        else if (activeSpell == "Contagion")
        {
            indic.text = "Contagion: Choose a mine to remove one miner from each worksite";
        }
        else if (activeSpell == "Earthquake")
        {
            indic.text = "Earthquake: Choose a mine or worksite to remove all miners from a work site";
        }
        else if (activeSpell == "Depletion")
        {
            indic.text = "Depletion: Choose a gem type to lose battery levels in";
        }
        else if (activeSpell == "Moratorium")
        {
            indic.text = "Moratorium: Choose a gem type to halt queue progression";
        }
        else if (activeSpell == "Imperfections")
        {
            indic.text = "Imperfections: Choose a gem type to half processing mana";
        }
        else if (activeSpell == "Inflation")
        {
            indic.text = "Inflation: Choose a gem type to buy battery levels in";
        }
        else if (activeSpell == "Stasis")
        {
            indic.text = "Stasis: Choose a gem type to halt all battery progress for";
        }
        else if (activeSpell == "Intensify")
        {
            indic.text = "Intensify: Choose a major or minor gem type to double battery advancement for all gems of that category";
        }
        else if(activeSpell == "Purge")
        {
            indic.text = "Purge: Choose spells to discard for mana (enter to quit)";
        }
        else if(activeSpell == "Spellsteal" && victim == -1)
        {
            indic.text = "Spellsteal: Choose a player to steal a spell from (1-" + PLAYERS + ")";
        }
        else if(activeSpell == "Spellsteal")
        {
            indic.text = "Spellsteal: Choose a spell to steal";
        }
        else if(activeSpell == "Suspension")
        {
            indic.text = "Suspension: Choose a player to delay their miner placement (1-" + PLAYERS + ")";
        }
        else if(activeSpell == "Pilfer")
        {
            indic.text = "Pilfer: Choose a player to steal mana from (1-" + PLAYERS + ")";
        }
        else if(activeSpell == "Casting")
        {
            indic.text = "Casting: Choose how you want to pay for the spell";
        }
        else if(activeSpell == "Plunder")
        {
            indic.text = "Plunder: Choose a gem in an opponent's queue to steal (q)";
        }
        else if(activeSpell == "Transmission")
        {
            indic.text = "Transmission: Choose a gem in your queue to exchange for battery levels";
        }
        else if(activeSpell == "Siphon")
        {
            indic.text = "Siphon: Choose a gem type to steal battery advancement from player " + (altTurn+1);
        }
        else if(activeSpell == "Transfiguration" && transfiguration[0]==-1)
        {
            indic.text = "Transfiguration: Select a gem in your queue to trade";
        }
        else if(activeSpell == "Transfiguration")
        {
            indic.text = "Transfiguration: Select gems in your opponent's queue to trade for (q)";
        }
        else if(activeSpell == "Advancement2")
        {
            indic.text = "Advancement: Select a gem type";
        }
        else if(activeSpell == "Advancement1")
        {
            indic.text = "Advancement: Select a stage in your queue for your gems to go to";
        }
        else if(activeSpell == "Transfusion2")
        {
            indic.text = "Transfusion: Select gems in your queue to process immediately";
        }
        else if(activeSpell == "Transfusion1")
        {
            indic.text = "Transfusion: Select whether to process your gems for battery or mana (1 or 2)";
        }
        else if(activeSpell == "Munificence")
        {
            indic.text = "Munificence: Select a card from the discard pile to take (d)";
        }
        else if(activeSpell == "Overload")
        {
            indic.text = "Overload: Select a gem in the final stage of your queue to give up";
        }
        else if(activeSpell == "Realignment")
        {
            indic.text = "Realignment: Select a gem type to get battery levels instead of " + gemTypeOf(players[turn].realigning);
        }
        
    }

    public static string gemTypeOf(int gemType)
    {
        switch (gemType)
        {
            case 0:
                return "Topaz";
            case 1:
                return "Ruby";
            case 2:
                return "Citrine";
            case 3:
                return "Emerald";
            case 4:
                return "Amethyst";
            case 5:
                return "Sapphire";
            case 6:
                return "Onyx";
            case 7:
                return "Diamond";
            default:
                return "Topaz";
        }
    }
}
