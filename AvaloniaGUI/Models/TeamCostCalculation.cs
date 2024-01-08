using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AvaloniaGUI.Models;

public class TeamCostCalculation
{
    //一个Up角色和一个Up专武的成本
    private const decimal CostPerFiveStarCharacter = 93m;
    private const decimal CostPerFiveStarWeapon = 67m;

    public class Team
    {
        private List<CharacterWithWeapon> _charactersWithWeapons = [];

        public void AddCharacter(CharacterWithWeapon characterWithWeapon)
        {
            _charactersWithWeapons.Add(characterWithWeapon);
        }

        public string PrintTotalCost()
        {
            var totalCost = _charactersWithWeapons.Sum(characterWithWeapon => characterWithWeapon.Cost);
            return $"全队总成本（以平均专票计）：{totalCost}";
        }
    }

    public class CharacterWithWeapon
    {
        public string CharacterName { get; set; }
        private bool IsCharacterFiveStar { get; set; }
        private bool IsWeaponFiveStar { get; set; }
        [Range(-1, 6)] private int 角色命座数 { get; set; }
        [Range(0, 5)] private int 专武叠影数 { get; set; }

        public decimal Cost
        {
            get
            {
                return (IsCharacterFiveStar, IsWeaponFiveStar) switch
                {
                    (false, false) => 0m,
                    (true, false) => (角色命座数 + 1) * CostPerFiveStarCharacter,
                    (false, true) => 专武叠影数 * CostPerFiveStarWeapon,
                    (true, true) => (角色命座数 + 1) * CostPerFiveStarCharacter + 专武叠影数 * CostPerFiveStarWeapon
                };
            }
        }


        protected internal CharacterWithWeapon(string characterName, bool isCharacterFiveStar = false, int 角色命座数 = -1,
            bool isWeaponFiveStar = false, int 专武叠影数 = 0)
        {
            CharacterName = characterName;
            IsCharacterFiveStar = isCharacterFiveStar;
            this.角色命座数 = 角色命座数;
            IsWeaponFiveStar = isWeaponFiveStar;
            this.专武叠影数 = 专武叠影数;
        }
    }
}