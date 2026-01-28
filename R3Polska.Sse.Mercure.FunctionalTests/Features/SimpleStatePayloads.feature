Feature: Simple State Payload Messages
    As a developer
    I want to publish simple state messages to Mercure
    So that the frontend can react to state changes

    Background:
        Given I am subscribed to the "scan" topic

    Scenario: Publish bag closing message
        When I publish a BagClosing message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "bag_closing"

    Scenario: Publish bag required message
        When I publish a BagRequiredPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "bag_required"

    Scenario: Publish barcode data invalid message
        When I publish a BarcodeDataInvalidPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "barcode_data_invalid"

    Scenario: Publish cancel drop message
        When I publish a CancelDropPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "cancel_drop"

    Scenario: Publish debouncing message
        When I publish a DebouncingPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "debouncing"

    Scenario: Publish finished message
        When I publish a FinishedPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "finished"

    Scenario: Publish logout message
        When I publish a LogoutPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "signed_out"

    Scenario: Publish scan limit message
        When I publish a ScanLimitPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "scan_limit"

    Scenario: Publish service enter message
        When I publish a ServiceEnterPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "service_enter"

    Scenario: Publish service exit message
        When I publish a ServiceExitPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "service_exit"

    Scenario: Publish session expired message
        When I publish a SessionExpiredPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "session_expired"

    Scenario: Publish signed in message
        When I publish a SignedInPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "signed_in"

    Scenario: Publish wait for bag message
        When I publish a WaitForBag message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "wait_for_bag"

    Scenario: Publish printer paper state message
        When I publish a PrinterPaperStatePayload message with paper state "low" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "paper_state_low"