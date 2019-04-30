# Start async test run and fetch the test run
TEST_RUN=$(appcenter-cli test run espresso --app "[ORG/APP NAME]" --devices [DEVICE SET ID] --app-path app/build/outputs/apk/app-debug.apk --test-series "master" --locale "en_US" --build-dir ~/Projects/ms_opensource/appcenter-sampleapp-android/app/build/outputs/apk/ --async | pcregrep -o1 -M 'Test run id: "(.+)"');
echo "Located new test run: $TEST_RUN"

# Trigger Logic App to monitor test run
echo "Triggering Logic App flow...";

# All paramaters here should be replaced with environment variables on the branch configuration
curl -H "Content-Type: application/json" \
-X POST -d "{\"token\":\"[APP CENTER TOKEN]\"," \
           "\"view_url\":\"/orgs/[ORG NAME]/apps/[APP NAME]/test/runs/$TEST_RUN\"," \
           "\"test_run\":\"/[ORG NAME]/[APP NAME]/test_runs/$TEST_RUN\"}" \
           "[LOGIC APP URL]"
