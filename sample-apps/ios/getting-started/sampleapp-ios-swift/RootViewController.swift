
import UIKit

class RootViewController: UIPageViewController, UIPageViewControllerDelegate, UIPageViewControllerDataSource {

    // give a list of service
    var services: [String] = []

    override func viewDidLoad() {
        super.viewDidLoad()

        // Here, we manually add our services to the Data Model
        services = [
            "welcomeViewController",
            "buildViewController",
            "testViewController",
            "distributeViewController",
            "crashViewController",
            "analyticsViewController",
            "pushViewController",
        ]

        delegate = self
        dataSource = self

        // set the starting viewController, in this case it is welcomeViewController
        let startingViewController = storyboard?.instantiateViewController(withIdentifier: "welcomeViewController")
        setViewControllers([startingViewController!],
                           direction: .forward,
                           animated: true,
                           completion: nil)
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
        // Dispose of any resources that can be recreated.
    }

    // MARK: - UIPageViewController navigation functions
    override func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()

        if let scrollView = view.subviews.filter({ $0 is UIScrollView }).first,
            let pageControl = view.subviews.filter({ $0 is UIPageControl }).first {
            scrollView.frame = view.bounds
            view.bringSubview(toFront: pageControl)
        }
    }

    // MARK: - UIPageViewController protocol functions

    func pageViewController(_: UIPageViewController, viewControllerBefore viewController: UIViewController) -> UIViewController? {
        let index = services.index(of: viewController.restorationIdentifier!)
        if (index == 0) || (index == NSNotFound) {
            return nil
        } else {
            return storyboard?.instantiateViewController(withIdentifier: services[index! - 1])
        }
    }

    func pageViewController(_: UIPageViewController,
                            viewControllerAfter viewController: UIViewController) -> UIViewController? {
        let index = services.index(of: viewController.restorationIdentifier!)
        if index! < services.count - 1 {
            return storyboard?.instantiateViewController(withIdentifier: services[index! + 1])
        } else {
            return nil
        }
    }

    func presentationCount(for _: UIPageViewController) -> Int {
        return services.count
    }

    func presentationIndex(for _: UIPageViewController) -> Int {
        return 0
    }
}
