using QuickEngine.Unity;
using System.Collections;
using UnityEngine;

public class QRoutineTest : MonoBehaviour
{

    private void Start()
    {

        QRoutines routine = QRoutines.Create(this)
            .WaitFor(RoutineMethod1())
            .WaitForSeconds(0.25f)
            .WaitFor(RoutineMethod2)
            .WaitForSeconds(0.25f);

        routine.WaitForSecondsRealtime(0.25f)
            .WaitFor(RoutineMethod1(), RoutineMethod1())
            .WaitFor(false, RoutineMethod2, RoutineMethod2)
            .WaitForFixedUpdate()
            .WaitFor(new WaitForSecondsRealtime(0.2f))
            .WaitFor(new WaitForSeconds(0.3f))
            .WaitFor(() => { Debug.Log("Execute code on the fly!"); })
            .WaitFor(() => { Debug.Log("Press Enter to continue..."); })
            .WaitForKeyDown(KeyCode.Return)
            .WaitFor(() => { Debug.Log("Thanks!"); })
            .WaitForFrames(5)
            .WaitFor(() => { Debug.Log("Left click to continue..."); })
            .WaitForMouseDown(0)
            .WaitFor(() => { Debug.Log("Thanks again!"); })
            .WaitFor(() =>
            {
                Debug.Log("Sleeping for 3 seconds on background thread...");
                System.Threading.Thread.Sleep(3000);
                Debug.Log("Done Sleeping");
            }, true)
            .WaitFor(() => { Debug.Log(string.Format("QRoutine is still Running: {0}", routine.IsRunning)); })
            .WaitFor(() => { Debug.Log("We are going to stop now."); })

            .WaitFor(() => { routine.Abort(); })
            .WaitFor(() => { Debug.Log("This will not be executed"); });

        QRoutines.Create()
            .WaitForQRoutine(routine)
            .WaitFor(() => { Debug.Log("Routine just finished executing."); })
            .WaitFor(() => { Debug.Log(string.Format("QRoutine is still Running: {0}", routine.IsRunning)); });

    }

    private IEnumerator RoutineMethod1()
    {
        Debug.Log("Routine Method 1");
        yield return Yielders.Seconds(0.1f);
        yield return null;
    }

    private void RoutineMethod2()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("Routine Method 2");
        }

    }
}
