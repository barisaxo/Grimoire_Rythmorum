using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterieHUD
{
    readonly public int PlayerMax;
    private int _playerCurrent;
    public int PlayerCurrent
    {
        get => _playerCurrent;
        set
        {
            _playerCurrent = value < 0 ? 0 : value;
            PlayerHealthBar.SetImageSize(new Vector2((float)((float)_playerCurrent / (float)PlayerMax) * 1.6f, .2f))
            .SetImageColor(new Color(1f - (float)((float)_playerCurrent / (float)PlayerMax), (float)((float)_playerCurrent / (float)PlayerMax), 0))
            ;
        }
    }

    readonly public int NMEMax;
    private int _nmeCurrent;
    public int NMECurrent
    {
        get => _nmeCurrent;
        set
        {
            _nmeCurrent = value < 0 ? 0 : value;
            NMEHealthBar.SetImageSize(new Vector2((float)((float)_nmeCurrent / (float)NMEMax) * 1.6f, .2f))
            .SetImageColor(new Color(1f - (float)((float)_nmeCurrent / (float)NMEMax), (float)((float)_nmeCurrent / (float)NMEMax), 0))
            ;
        }
    }

    public BatterieHUD(int playerHealth, int playerCurrent, int nmeHealth)
    {
        PlayerMax = playerHealth;
        PlayerCurrent = playerCurrent;
        NMECurrent = NMEMax = nmeHealth;
    }

    public void SelfDestruct()
    {
        Parent.SelfDestruct();
    }

    public void Initialize()
    {
        _ = Parent.Canvas;
        _ = NMEHealthBar;
        _ = PlayerHealthBar;
    }

    private Card _parent;
    public Card Parent => _parent ??= new(nameof(BatterieHUD), null);

    private Card _playerHealthBar;
    public Card PlayerHealthBar => _playerHealthBar ??= Parent.CreateChild(nameof(PlayerHealthBar), Parent.Canvas)
        .SetImagePosition(-Cam.Io.UICamera.orthographicSize, -Cam.Io.UICamera.orthographicSize + 1f)
        ;

    private Card _nmeHealthBar;
    public Card NMEHealthBar => _nmeHealthBar ??= Parent.CreateChild(nameof(NMEHealthBar), Parent.Canvas)
        .SetImagePosition(Cam.Io.UICamera.orthographicSize, -Cam.Io.UICamera.orthographicSize + 1f)
        ;


}
