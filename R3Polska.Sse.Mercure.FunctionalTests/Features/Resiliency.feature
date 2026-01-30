Feature: HttpClient Resiliency
    As a developer
    I want the MercureService to be resilient to temporary failures
    So that publish operations retry when Mercure is temporarily unavailable

    @resiliency
    Scenario: Publish operation retries and succeeds when Mercure recovers
        Given I have a MercureService with 3 retries and 3 second intervals
        When I stop the Mercure container
        And I publish a ReadyPayload message to the "resiliency-test" topic in the background
        And I wait for 2 seconds
        And I start the Mercure container
        Then the publish operation should complete successfully within 20 seconds

    @resiliency
    Scenario: Publish operation fails after retries exhausted
        Given I have a MercureService with 2 retries and 1 second intervals
        When I stop the Mercure container
        And I publish a ReadyPayload message to the "resiliency-fail-test" topic and expect failure
        Then the publish operation should fail
        # Cleanup: restart Mercure for other tests
        And the Mercure container is restarted