Feature: Publish Messages to Mercure
    As a developer
    I want to publish messages to Mercure hub
    So that subscribers can receive real-time updates

    Background:
        Given I am subscribed to the "scan" topic

    @smoke
    Scenario: Publish a ready state message
        When I publish a ReadyPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "ready"

    @smoke
    Scenario: Publish a message with custom ID
        When I publish a ReadyPayload message to the "scan" topic with ID "custom-123"
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "ready"

    Scenario: Publish awaits drop message with EAN
        When I publish an AwaitsDropPayload message with EAN "5901234123457" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "awaits_drop"
        And the received message should have EAN "5901234123457"

    Scenario: Publish product detection message
        When I publish a ProductDetectionPayload message with EAN "9780123456789" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "product_detection"
        And the received message should have EAN "9780123456789"

    Scenario: Publish unknown product message
        When I publish an UnknownProductPayload message with EAN "0000000000000" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "product_unknown"

    Scenario: Publish product not allowed message
        When I publish a ProductNotAllowedPayload message with EAN "1111111111111" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "product_not_allowed"

    Scenario: Publish printer status message
        When I publish a PrinterStatusPayload message with state "printing" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "printer_printing"

    Scenario: Publish printer status with job details
        When I publish a PrinterStatusPayload message with state "printing", job ID "JOB-001" and wait time 30 to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "printer_printing"

    Scenario: Publish internet connectivity message
        When I publish an InternetConnectivityPayload message with state "online" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "inet_connection_online"

    Scenario: Publish scanner connection message
        When I publish a ScannerConnectionPayload message with state "scanner_ok" to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "scanner_ok"

    Scenario: Publish finishing message with Polish text
        When I publish a FinishingPayload message to the "scan" topic
        Then the subscriber should receive a message within 5 seconds
        And the received message should have state "finishing"
        And the received message should have message "Trwa kończenie."