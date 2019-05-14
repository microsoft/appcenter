
import UIKit
import AppCenterCrashes

class CrashViewController: UIViewController {

    @IBOutlet weak var fatalErrorButton: UIButton!

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view, typically from a nib.
        fatalErrorButton.layer.cornerRadius = 10.0

        navigationController?.navigationBar.barTintColor = UIColor.black
        navigationController?.navigationBar.isTranslucent = false
    }

    @IBAction func crashButtonTapped(_: UIButton) {
        presentCrashAlert()
    }

    // - MARK: Alert Functions
    func presentCrashAlert() {
        let alert = UIAlertController(title: "The app will close",
                                      message: "A crash report will be sent when you reopen the app.",
                                      preferredStyle: UIAlertControllerStyle.alert)

        // Cancel Button
        alert.addAction(UIAlertAction(title: "Cancel",
                                      style: UIAlertActionStyle.default,
                                      handler: { _ in alert.dismiss(animated: true, completion: nil)
        }))
        // Crash App button
        alert.addAction(UIAlertAction(title: "Crash app",
                                      style: UIAlertActionStyle.destructive,
                                      handler: { _ in alert.dismiss(animated: true, completion: nil)
                                          // generate test crash
                                          MSCrashes.generateTestCrash()
                                          fatalError()
        }))

        present(alert, animated: true, completion: nil)
    }
}
