using UnityEngine;

public class ComputerTestScript : MonoBehaviour
{

    public ComputerScript ControlledComputer;
    public BreakerScript ControlledBreaker;
    public GameObject LaserGrid;

    // Start is called before the first frame update
    void Start()
    {

        Invoke(nameof(RunTests), 2);

    }

    private void RunTests()
    {

        // Try to authenticate on a computer with no power
        bool noPowerAuthResult = ControlledComputer.TrySignIn("jtester", "testing", out string noPowerAuthErrorMessage);
        print($"No power authentication result: {noPowerAuthResult} ({noPowerAuthErrorMessage})");

        // Try to disable the laser grid
        ControlledComputer.DisableLaserGrid();
        print($"No power disable laser grid result: {LaserGrid}");

        // Turn the power on
        ControlledBreaker.Interact(null);

        // Try to authenticate with a nonexistent username
        bool nonexistentAuthResult = ControlledComputer.TrySignIn("", "testing", out string nonexistentAuthErrorMessage);
        print($"Nonexistent username authentication result: {nonexistentAuthResult} ({nonexistentAuthErrorMessage})");

        // Try to authenticate with a bad password
        bool badPassAuthResult = ControlledComputer.TrySignIn("jtester", "", out string badPassAuthErrorMessage);
        print($"Bad password authentication result: {badPassAuthResult} ({badPassAuthErrorMessage})");

        // Authenticate to a "non-admin" user
        bool noAdminAuthResult = ControlledComputer.TrySignIn("jtester", "testing", out string noAdminAuthMessage);
        print($"No admin authentication result: {noAdminAuthResult} ({noAdminAuthMessage})");

        // Attempt to disable the laser grid as this user
        ControlledComputer.DisableLaserGrid();
        print($"No admin disable laser grid result: {LaserGrid}");

        // Sign out
        ControlledComputer.SignOut();

        // Authenticate to an "admin" user
        bool adminAuthResult = ControlledComputer.TrySignIn("ldisabler", "lasers", out string adminAuthMessage);
        print($"Admin authentication result: {adminAuthResult} ({adminAuthMessage})");

        // Attempt to disable the laser grid as this user
        ControlledComputer.DisableLaserGrid();
        print($"Admin disable laser grid result: {LaserGrid}");

    }

}
