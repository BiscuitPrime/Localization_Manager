using UnityEngine;

public class TestPrinter : MonoBehaviour
{
    [SerializeField] private string displaytext1 = "T_TEST_T1";
    [SerializeField] private string displaytext2 = "T_TEST_T1";
    [SerializeField] private bool test=false;

    private void Update()
    {
        if (test)
        {
            test = false;
            Print();
        }
    }
    private void Print()
    {
        print("TEST PRINTER");
        print("Test 1 : " + LocalizationManager.Instance.LocalText(displaytext1));
        print("Test 2 : " + LocalizationManager.Instance.LocalText(displaytext2));
    }
}
