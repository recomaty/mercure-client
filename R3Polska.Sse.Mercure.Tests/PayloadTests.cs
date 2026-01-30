using System.Text.Json;
using R3Polska.Sse.Mercure.Message;
using Shouldly;

namespace R3Polska.Sse.Mercure.Tests;

public class PayloadTests
{
    #region Simple State Payloads

    [Fact]
    public void ReadyPayload_State_ShouldBeReady()
    {
        var payload = new ReadyPayload();
        payload.State.ShouldBe("ready");
    }

    [Fact]
    public void ReadyPayload_ShouldSerializeCorrectly()
    {
        var payload = new ReadyPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"ready\"");
    }

    [Fact]
    public void BagClosing_State_ShouldBeBagClosing()
    {
        var payload = new BagClosing();
        payload.State.ShouldBe("bag_closing");
    }

    [Fact]
    public void BagClosing_ShouldSerializeCorrectly()
    {
        var payload = new BagClosing();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"bag_closing\"");
    }

    [Fact]
    public void BagRequiredPayload_State_ShouldBeBagRequired()
    {
        var payload = new BagRequiredPayload();
        payload.State.ShouldBe("bag_required");
    }

    [Fact]
    public void BagRequiredPayload_ShouldSerializeCorrectly()
    {
        var payload = new BagRequiredPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"bag_required\"");
    }

    [Fact]
    public void BarcodeDataInvalidPayload_State_ShouldBeBarcodeDataInvalid()
    {
        var payload = new BarcodeDataInvalidPayload();
        payload.State.ShouldBe("barcode_data_invalid");
    }

    [Fact]
    public void BarcodeDataInvalidPayload_ShouldSerializeCorrectly()
    {
        var payload = new BarcodeDataInvalidPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"barcode_data_invalid\"");
    }

    [Fact]
    public void CancelDropPayload_State_ShouldBeCancelDrop()
    {
        var payload = new CancelDropPayload();
        payload.State.ShouldBe("cancel_drop");
    }

    [Fact]
    public void CancelDropPayload_ShouldSerializeCorrectly()
    {
        var payload = new CancelDropPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"cancel_drop\"");
    }

    [Fact]
    public void DebouncingPayload_State_ShouldBeDebouncing()
    {
        var payload = new DebouncingPayload();
        payload.State.ShouldBe("debouncing");
    }

    [Fact]
    public void DebouncingPayload_ShouldSerializeCorrectly()
    {
        var payload = new DebouncingPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"debouncing\"");
    }

    [Fact]
    public void FinishedPayload_State_ShouldBeFinished()
    {
        var payload = new FinishedPayload();
        payload.State.ShouldBe("finished");
    }

    [Fact]
    public void FinishedPayload_ShouldSerializeCorrectly()
    {
        var payload = new FinishedPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"finished\"");
    }

    [Fact]
    public void LogoutPayload_State_ShouldBeSignedOut()
    {
        var payload = new LogoutPayload();
        payload.State.ShouldBe("signed_out");
    }

    [Fact]
    public void LogoutPayload_ShouldSerializeCorrectly()
    {
        var payload = new LogoutPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"signed_out\"");
    }

    [Fact]
    public void ScanLimitPayload_State_ShouldBeScanLimit()
    {
        var payload = new ScanLimitPayload();
        payload.State.ShouldBe("scan_limit");
    }

    [Fact]
    public void ScanLimitPayload_ShouldSerializeCorrectly()
    {
        var payload = new ScanLimitPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"scan_limit\"");
    }

    [Fact]
    public void ServiceEnterPayload_State_ShouldBeServiceEnter()
    {
        var payload = new ServiceEnterPayload();
        payload.State.ShouldBe("service_enter");
    }

    [Fact]
    public void ServiceEnterPayload_ShouldSerializeCorrectly()
    {
        var payload = new ServiceEnterPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"service_enter\"");
    }

    [Fact]
    public void ServiceExitPayload_State_ShouldBeServiceExit()
    {
        var payload = new ServiceExitPayload();
        payload.State.ShouldBe("service_exit");
    }

    [Fact]
    public void ServiceExitPayload_ShouldSerializeCorrectly()
    {
        var payload = new ServiceExitPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"service_exit\"");
    }

    [Fact]
    public void SessionExpiredPayload_State_ShouldBeSessionExpired()
    {
        var payload = new SessionExpiredPayload();
        payload.State.ShouldBe("session_expired");
    }

    [Fact]
    public void SessionExpiredPayload_ShouldSerializeCorrectly()
    {
        var payload = new SessionExpiredPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"session_expired\"");
    }

    [Fact]
    public void SignedInPayload_State_ShouldBeSignedIn()
    {
        var payload = new SignedInPayload();
        payload.State.ShouldBe("signed_in");
    }

    [Fact]
    public void SignedInPayload_ShouldSerializeCorrectly()
    {
        var payload = new SignedInPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"signed_in\"");
    }

    [Fact]
    public void WaitForBag_State_ShouldBeWaitForBag()
    {
        var payload = new WaitForBag();
        payload.State.ShouldBe("wait_for_bag");
    }

    [Fact]
    public void WaitForBag_ShouldSerializeCorrectly()
    {
        var payload = new WaitForBag();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"wait_for_bag\"");
    }

    #endregion

    #region Finishing Payload

    [Fact]
    public void FinishingPayload_State_ShouldBeFinishing()
    {
        var payload = new FinishingPayload();
        payload.State.ShouldBe("finishing");
    }

    [Fact]
    public void FinishingPayload_Message_ShouldBePolishText()
    {
        var payload = new FinishingPayload();
        payload.Message.ShouldBe("Trwa kończenie.");
    }

    [Fact]
    public void FinishingPayload_ShouldSerializeCorrectly()
    {
        var payload = new FinishingPayload();
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"finishing\"");
        json.ShouldContain("\"message\":");
    }

    #endregion

    #region EAN-based Payloads

    [Fact]
    public void AwaitsDropPayload_State_ShouldBeAwaitsDrop()
    {
        var payload = new AwaitsDropPayload("1234567890123");
        payload.State.ShouldBe("awaits_drop");
    }

    [Fact]
    public void AwaitsDropPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "1234567890123";
        var payload = new AwaitsDropPayload(ean);
        payload.Ean.ShouldBe(ean);
    }

    [Fact]
    public void AwaitsDropPayload_ShouldSerializeCorrectly()
    {
        var payload = new AwaitsDropPayload("1234567890123");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"awaits_drop\"");
        json.ShouldContain("\"ean\":\"1234567890123\"");
    }

    [Theory]
    [InlineData("")]
    [InlineData("5901234123457")]
    [InlineData("978-3-16-148410-0")]
    public void AwaitsDropPayload_ShouldAcceptVariousEanFormats(string ean)
    {
        var payload = new AwaitsDropPayload(ean);
        payload.Ean.ShouldBe(ean);
    }

    [Fact]
    public void ProductDetectionPayload_State_ShouldBeProductDetection()
    {
        var payload = new ProductDetectionPayload("1234567890123");
        payload.State.ShouldBe("product_detection");
    }

    [Fact]
    public void ProductDetectionPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "9876543210987";
        var payload = new ProductDetectionPayload(ean);
        payload.Ean.ShouldBe(ean);
    }

    [Fact]
    public void ProductDetectionPayload_ShouldSerializeCorrectly()
    {
        var payload = new ProductDetectionPayload("9876543210987");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"product_detection\"");
        json.ShouldContain("\"ean\":\"9876543210987\"");
    }

    [Fact]
    public void ProductNotAllowedPayload_State_ShouldBeProductNotAllowed()
    {
        var payload = new ProductNotAllowedPayload("1234567890123");
        payload.State.ShouldBe("product_not_allowed");
    }

    [Fact]
    public void ProductNotAllowedPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "5555555555555";
        var payload = new ProductNotAllowedPayload(ean);
        payload.Ean.ShouldBe(ean);
    }

    [Fact]
    public void ProductNotAllowedPayload_ShouldSerializeCorrectly()
    {
        var payload = new ProductNotAllowedPayload("5555555555555");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"product_not_allowed\"");
        json.ShouldContain("\"ean\":\"5555555555555\"");
    }

    [Fact]
    public void UnknownProductPayload_State_ShouldBeProductUnknown()
    {
        var payload = new UnknownProductPayload("1234567890123");
        payload.State.ShouldBe("product_unknown");
    }

    [Fact]
    public void UnknownProductPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "0000000000000";
        var payload = new UnknownProductPayload(ean);
        payload.Ean.ShouldBe(ean);
    }

    [Fact]
    public void UnknownProductPayload_ShouldSerializeCorrectly()
    {
        var payload = new UnknownProductPayload("0000000000000");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"product_unknown\"");
        json.ShouldContain("\"ean\":\"0000000000000\"");
    }

    #endregion

    #region Dynamic State Payloads

    [Theory]
    [InlineData("online", "inet_connection_online")]
    [InlineData("offline", "inet_connection_offline")]
    [InlineData("slow", "inet_connection_slow")]
    public void InternetConnectivityPayload_State_ShouldContainDynamicState(string state, string expected)
    {
        var payload = new InternetConnectivityPayload(state);
        payload.State.ShouldBe(expected);
    }

    [Fact]
    public void InternetConnectivityPayload_ShouldSerializeCorrectly()
    {
        var payload = new InternetConnectivityPayload("online");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"inet_connection_online\"");
    }

    [Theory]
    [InlineData("ok", "paper_state_ok")]
    [InlineData("low", "paper_state_low")]
    [InlineData("empty", "paper_state_empty")]
    public void PrinterPaperStatePayload_State_ShouldContainDynamicState(string paperState, string expected)
    {
        var payload = new PrinterPaperStatePayload(paperState);
        payload.State.ShouldBe(expected);
    }

    [Fact]
    public void PrinterPaperStatePayload_ShouldSerializeCorrectly()
    {
        var payload = new PrinterPaperStatePayload("low");
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"paper_state_low\"");
    }

    [Theory]
    [InlineData("ready", "printer_ready")]
    [InlineData("printing", "printer_printing")]
    [InlineData("error", "printer_error")]
    [InlineData("offline", "printer_offline")]
    public void PrinterStatusPayload_State_ShouldContainDynamicState(string printerState, string expected)
    {
        var payload = new PrinterStatusPayload(printerState);
        payload.State.ShouldBe(expected);
    }

    [Fact]
    public void PrinterStatusPayload_SingleParam_ShouldHaveNullOptionalFields()
    {
        var payload = new PrinterStatusPayload("ready");
        payload.PrintJobId.ShouldBeNull();
        payload.WaitTimeInSeconds.ShouldBeNull();
        payload.PaperStatus.ShouldBeNull();
    }

    [Fact]
    public void PrinterStatusPayload_ThreeParams_ShouldSetJobIdAndWaitTime()
    {
        var payload = new PrinterStatusPayload("printing", "job123", 30);
        payload.State.ShouldBe("printer_printing");
        payload.PrintJobId.ShouldBe("job123");
        payload.WaitTimeInSeconds.ShouldBe(30);
    }

    [Fact]
    public void PrinterStatusPayload_PaperStatus_CanBeSet()
    {
        var payload = new PrinterStatusPayload("ready")
        {
            PaperStatus = 1
        };
        payload.PaperStatus.ShouldBe(1);
    }

    [Fact]
    public void PrinterStatusPayload_ShouldSerializeCorrectly()
    {
        var payload = new PrinterStatusPayload("printing", "job456", 60)
        {
            PaperStatus = 2
        };
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"printer_printing\"");
        json.ShouldContain("\"paper_state\":2");
    }

    [Theory]
    [InlineData("scanner_ok")]
    [InlineData("scanner_missing")]
    public void ScannerConnectionPayload_State_ShouldBeSettableAsRequired(string state)
    {
        var payload = new ScannerConnectionPayload { State = state };
        payload.State.ShouldBe(state);
    }

    [Fact]
    public void ScannerConnectionPayload_ShouldSerializeCorrectly()
    {
        var payload = new ScannerConnectionPayload { State = "scanner_ok" };
        var json = JsonSerializer.Serialize(payload);
        json.ShouldContain("\"state\":\"scanner_ok\"");
    }

    #endregion

    #region Interface Implementation

    [Fact]
    public void AllPayloads_ShouldImplementIMercureMessagePayload()
    {
        new ReadyPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new BagClosing().ShouldBeAssignableTo<IMercureMessagePayload>();
        new BagRequiredPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new BarcodeDataInvalidPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new CancelDropPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new DebouncingPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new FinishedPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new FinishingPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new LogoutPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new ScanLimitPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new ServiceEnterPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new ServiceExitPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new SessionExpiredPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new SignedInPayload().ShouldBeAssignableTo<IMercureMessagePayload>();
        new WaitForBag().ShouldBeAssignableTo<IMercureMessagePayload>();
        new AwaitsDropPayload("ean").ShouldBeAssignableTo<IMercureMessagePayload>();
        new ProductDetectionPayload("ean").ShouldBeAssignableTo<IMercureMessagePayload>();
        new ProductNotAllowedPayload("ean").ShouldBeAssignableTo<IMercureMessagePayload>();
        new UnknownProductPayload("ean").ShouldBeAssignableTo<IMercureMessagePayload>();
        new InternetConnectivityPayload("online").ShouldBeAssignableTo<IMercureMessagePayload>();
        new PrinterPaperStatePayload("ok").ShouldBeAssignableTo<IMercureMessagePayload>();
        new PrinterStatusPayload("ready").ShouldBeAssignableTo<IMercureMessagePayload>();
        new ScannerConnectionPayload { State = "scanner_ok" }.ShouldBeAssignableTo<IMercureMessagePayload>();
    }

    #endregion
}