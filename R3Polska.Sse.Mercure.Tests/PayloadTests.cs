using System.Text.Json;
using R3Polska.Sse.Mercure.Message;

namespace R3Polska.Sse.Mercure.Tests;

public class PayloadTests
{
    #region Simple State Payloads

    [Fact]
    public void ReadyPayload_State_ShouldBeReady()
    {
        var payload = new ReadyPayload();
        Assert.Equal("ready", payload.State);
    }

    [Fact]
    public void ReadyPayload_ShouldSerializeCorrectly()
    {
        var payload = new ReadyPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"ready\"", json);
    }

    [Fact]
    public void BagClosing_State_ShouldBeBagClosing()
    {
        var payload = new BagClosing();
        Assert.Equal("bag_closing", payload.State);
    }

    [Fact]
    public void BagClosing_ShouldSerializeCorrectly()
    {
        var payload = new BagClosing();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"bag_closing\"", json);
    }

    [Fact]
    public void BagRequiredPayload_State_ShouldBeBagRequired()
    {
        var payload = new BagRequiredPayload();
        Assert.Equal("bag_required", payload.State);
    }

    [Fact]
    public void BagRequiredPayload_ShouldSerializeCorrectly()
    {
        var payload = new BagRequiredPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"bag_required\"", json);
    }

    [Fact]
    public void BarcodeDataInvalidPayload_State_ShouldBeBarcodeDataInvalid()
    {
        var payload = new BarcodeDataInvalidPayload();
        Assert.Equal("barcode_data_invalid", payload.State);
    }

    [Fact]
    public void BarcodeDataInvalidPayload_ShouldSerializeCorrectly()
    {
        var payload = new BarcodeDataInvalidPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"barcode_data_invalid\"", json);
    }

    [Fact]
    public void CancelDropPayload_State_ShouldBeCancelDrop()
    {
        var payload = new CancelDropPayload();
        Assert.Equal("cancel_drop", payload.State);
    }

    [Fact]
    public void CancelDropPayload_ShouldSerializeCorrectly()
    {
        var payload = new CancelDropPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"cancel_drop\"", json);
    }

    [Fact]
    public void DebouncingPayload_State_ShouldBeDebouncing()
    {
        var payload = new DebouncingPayload();
        Assert.Equal("debouncing", payload.State);
    }

    [Fact]
    public void DebouncingPayload_ShouldSerializeCorrectly()
    {
        var payload = new DebouncingPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"debouncing\"", json);
    }

    [Fact]
    public void FinishedPayload_State_ShouldBeFinished()
    {
        var payload = new FinishedPayload();
        Assert.Equal("finished", payload.State);
    }

    [Fact]
    public void FinishedPayload_ShouldSerializeCorrectly()
    {
        var payload = new FinishedPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"finished\"", json);
    }

    [Fact]
    public void LogoutPayload_State_ShouldBeSignedOut()
    {
        var payload = new LogoutPayload();
        Assert.Equal("signed_out", payload.State);
    }

    [Fact]
    public void LogoutPayload_ShouldSerializeCorrectly()
    {
        var payload = new LogoutPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"signed_out\"", json);
    }

    [Fact]
    public void ScanLimitPayload_State_ShouldBeScanLimit()
    {
        var payload = new ScanLimitPayload();
        Assert.Equal("scan_limit", payload.State);
    }

    [Fact]
    public void ScanLimitPayload_ShouldSerializeCorrectly()
    {
        var payload = new ScanLimitPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"scan_limit\"", json);
    }

    [Fact]
    public void ServiceEnterPayload_State_ShouldBeServiceEnter()
    {
        var payload = new ServiceEnterPayload();
        Assert.Equal("service_enter", payload.State);
    }

    [Fact]
    public void ServiceEnterPayload_ShouldSerializeCorrectly()
    {
        var payload = new ServiceEnterPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"service_enter\"", json);
    }

    [Fact]
    public void ServiceExitPayload_State_ShouldBeServiceExit()
    {
        var payload = new ServiceExitPayload();
        Assert.Equal("service_exit", payload.State);
    }

    [Fact]
    public void ServiceExitPayload_ShouldSerializeCorrectly()
    {
        var payload = new ServiceExitPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"service_exit\"", json);
    }

    [Fact]
    public void SessionExpiredPayload_State_ShouldBeSessionExpired()
    {
        var payload = new SessionExpiredPayload();
        Assert.Equal("session_expired", payload.State);
    }

    [Fact]
    public void SessionExpiredPayload_ShouldSerializeCorrectly()
    {
        var payload = new SessionExpiredPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"session_expired\"", json);
    }

    [Fact]
    public void SignedInPayload_State_ShouldBeSignedIn()
    {
        var payload = new SignedInPayload();
        Assert.Equal("signed_in", payload.State);
    }

    [Fact]
    public void SignedInPayload_ShouldSerializeCorrectly()
    {
        var payload = new SignedInPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"signed_in\"", json);
    }

    [Fact]
    public void WaitForBag_State_ShouldBeWaitForBag()
    {
        var payload = new WaitForBag();
        Assert.Equal("wait_for_bag", payload.State);
    }

    [Fact]
    public void WaitForBag_ShouldSerializeCorrectly()
    {
        var payload = new WaitForBag();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"wait_for_bag\"", json);
    }

    #endregion

    #region Finishing Payload

    [Fact]
    public void FinishingPayload_State_ShouldBeFinishing()
    {
        var payload = new FinishingPayload();
        Assert.Equal("finishing", payload.State);
    }

    [Fact]
    public void FinishingPayload_Message_ShouldBePolishText()
    {
        var payload = new FinishingPayload();
        Assert.Equal("Trwa kończenie.", payload.Message);
    }

    [Fact]
    public void FinishingPayload_ShouldSerializeCorrectly()
    {
        var payload = new FinishingPayload();
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"finishing\"", json);
        Assert.Contains("\"message\":", json);
    }

    #endregion

    #region EAN-based Payloads

    [Fact]
    public void AwaitsDropPayload_State_ShouldBeAwaitsDrop()
    {
        var payload = new AwaitsDropPayload("1234567890123");
        Assert.Equal("awaits_drop", payload.State);
    }

    [Fact]
    public void AwaitsDropPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "1234567890123";
        var payload = new AwaitsDropPayload(ean);
        Assert.Equal(ean, payload.Ean);
    }

    [Fact]
    public void AwaitsDropPayload_ShouldSerializeCorrectly()
    {
        var payload = new AwaitsDropPayload("1234567890123");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"awaits_drop\"", json);
        Assert.Contains("\"ean\":\"1234567890123\"", json);
    }

    [Theory]
    [InlineData("")]
    [InlineData("5901234123457")]
    [InlineData("978-3-16-148410-0")]
    public void AwaitsDropPayload_ShouldAcceptVariousEanFormats(string ean)
    {
        var payload = new AwaitsDropPayload(ean);
        Assert.Equal(ean, payload.Ean);
    }

    [Fact]
    public void ProductDetectionPayload_State_ShouldBeProductDetection()
    {
        var payload = new ProductDetectionPayload("1234567890123");
        Assert.Equal("product_detection", payload.State);
    }

    [Fact]
    public void ProductDetectionPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "9876543210987";
        var payload = new ProductDetectionPayload(ean);
        Assert.Equal(ean, payload.Ean);
    }

    [Fact]
    public void ProductDetectionPayload_ShouldSerializeCorrectly()
    {
        var payload = new ProductDetectionPayload("9876543210987");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"product_detection\"", json);
        Assert.Contains("\"ean\":\"9876543210987\"", json);
    }

    [Fact]
    public void ProductNotAllowedPayload_State_ShouldBeProductNotAllowed()
    {
        var payload = new ProductNotAllowedPayload("1234567890123");
        Assert.Equal("product_not_allowed", payload.State);
    }

    [Fact]
    public void ProductNotAllowedPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "5555555555555";
        var payload = new ProductNotAllowedPayload(ean);
        Assert.Equal(ean, payload.Ean);
    }

    [Fact]
    public void ProductNotAllowedPayload_ShouldSerializeCorrectly()
    {
        var payload = new ProductNotAllowedPayload("5555555555555");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"product_not_allowed\"", json);
        Assert.Contains("\"ean\":\"5555555555555\"", json);
    }

    [Fact]
    public void UnknownProductPayload_State_ShouldBeProductUnknown()
    {
        var payload = new UnknownProductPayload("1234567890123");
        Assert.Equal("product_unknown", payload.State);
    }

    [Fact]
    public void UnknownProductPayload_Ean_ShouldBeSetCorrectly()
    {
        var ean = "0000000000000";
        var payload = new UnknownProductPayload(ean);
        Assert.Equal(ean, payload.Ean);
    }

    [Fact]
    public void UnknownProductPayload_ShouldSerializeCorrectly()
    {
        var payload = new UnknownProductPayload("0000000000000");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"product_unknown\"", json);
        Assert.Contains("\"ean\":\"0000000000000\"", json);
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
        Assert.Equal(expected, payload.State);
    }

    [Fact]
    public void InternetConnectivityPayload_ShouldSerializeCorrectly()
    {
        var payload = new InternetConnectivityPayload("online");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"inet_connection_online\"", json);
    }

    [Theory]
    [InlineData("ok", "paper_state_ok")]
    [InlineData("low", "paper_state_low")]
    [InlineData("empty", "paper_state_empty")]
    public void PrinterPaperStatePayload_State_ShouldContainDynamicState(string paperState, string expected)
    {
        var payload = new PrinterPaperStatePayload(paperState);
        Assert.Equal(expected, payload.State);
    }

    [Fact]
    public void PrinterPaperStatePayload_ShouldSerializeCorrectly()
    {
        var payload = new PrinterPaperStatePayload("low");
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"paper_state_low\"", json);
    }

    [Theory]
    [InlineData("ready", "printer_ready")]
    [InlineData("printing", "printer_printing")]
    [InlineData("error", "printer_error")]
    [InlineData("offline", "printer_offline")]
    public void PrinterStatusPayload_State_ShouldContainDynamicState(string printerState, string expected)
    {
        var payload = new PrinterStatusPayload(printerState);
        Assert.Equal(expected, payload.State);
    }

    [Fact]
    public void PrinterStatusPayload_SingleParam_ShouldHaveNullOptionalFields()
    {
        var payload = new PrinterStatusPayload("ready");
        Assert.Null(payload.PrintJobId);
        Assert.Null(payload.WaitTimeInSeconds);
        Assert.Null(payload.PaperStatus);
    }

    [Fact]
    public void PrinterStatusPayload_ThreeParams_ShouldSetJobIdAndWaitTime()
    {
        var payload = new PrinterStatusPayload("printing", "job123", 30);
        Assert.Equal("printer_printing", payload.State);
        Assert.Equal("job123", payload.PrintJobId);
        Assert.Equal(30, payload.WaitTimeInSeconds);
    }

    [Fact]
    public void PrinterStatusPayload_PaperStatus_CanBeSet()
    {
        var payload = new PrinterStatusPayload("ready")
        {
            PaperStatus = 1
        };
        Assert.Equal(1, payload.PaperStatus);
    }

    [Fact]
    public void PrinterStatusPayload_ShouldSerializeCorrectly()
    {
        var payload = new PrinterStatusPayload("printing", "job456", 60)
        {
            PaperStatus = 2
        };
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"printer_printing\"", json);
        Assert.Contains("\"paper_state\":2", json);
    }

    [Theory]
    [InlineData("scanner_ok")]
    [InlineData("scanner_missing")]
    public void ScannerConnectionPayload_State_ShouldBeSettableAsRequired(string state)
    {
        var payload = new ScannerConnectionPayload { State = state };
        Assert.Equal(state, payload.State);
    }

    [Fact]
    public void ScannerConnectionPayload_ShouldSerializeCorrectly()
    {
        var payload = new ScannerConnectionPayload { State = "scanner_ok" };
        var json = JsonSerializer.Serialize(payload);
        Assert.Contains("\"state\":\"scanner_ok\"", json);
    }

    #endregion

    #region Interface Implementation

    [Fact]
    public void AllPayloads_ShouldImplementIMercureMessagePayload()
    {
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ReadyPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new BagClosing());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new BagRequiredPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new BarcodeDataInvalidPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new CancelDropPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new DebouncingPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new FinishedPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new FinishingPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new LogoutPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ScanLimitPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ServiceEnterPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ServiceExitPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new SessionExpiredPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new SignedInPayload());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new WaitForBag());
        Assert.IsAssignableFrom<IMercureMessagePayload>(new AwaitsDropPayload("ean"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ProductDetectionPayload("ean"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ProductNotAllowedPayload("ean"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new UnknownProductPayload("ean"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new InternetConnectivityPayload("online"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new PrinterPaperStatePayload("ok"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new PrinterStatusPayload("ready"));
        Assert.IsAssignableFrom<IMercureMessagePayload>(new ScannerConnectionPayload { State = "scanner_ok" });
    }

    #endregion
}