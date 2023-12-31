﻿using AvaloniaGUI.Models;
using ReactiveUI;


namespace AvaloniaGUI.ViewModels.Others;

public class VmHome : ViewModelBase
{
    public string OsVersion { get; } = Models.Home.OsVersion;

    private string _tempInfo = "暂无信息";

    public string TempInfo
    {
        get => _tempInfo;
        set => this.RaiseAndSetIfChanged(ref _tempInfo, value);
    }

    public void UpdateTempInfo()
    {
        TeamCostCalculation.Team testTeam = new();
        var 希儿 = new TeamCostCalculation.CharacterWithWeapon("希儿", true, 0, true, 5);
        testTeam.AddCharacter(希儿);


        TempInfo = testTeam.PrintTotalCost();
    }
}