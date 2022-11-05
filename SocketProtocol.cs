// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: SocketProtocol.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace SocketProtocol {

  /// <summary>Holder for reflection information generated from SocketProtocol.proto</summary>
  public static partial class SocketProtocolReflection {

    #region Descriptor
    /// <summary>File descriptor for SocketProtocol.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SocketProtocolReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRTb2NrZXRQcm90b2NvbC5wcm90bxIOU29ja2V0UHJvdG9jb2wiywIKCE1h",
            "aW5QYWNrEjAKC1JlcXVlc3RDb2RlGAEgASgOMhsuU29ja2V0UHJvdG9jb2wu",
            "UmVxdWVzdENvZGUSLgoKQWN0aW9uQ29kZRgCIAEoDjIaLlNvY2tldFByb3Rv",
            "Y29sLkFjdGlvbkNvZGUSLgoKUmV0dXJuQ29kZRgDIAEoDjIaLlNvY2tldFBy",
            "b3RvY29sLlJldHVybkNvZGUSMgoMUmVnaXN0ZXJQYWNrGAQgASgLMhwuU29j",
            "a2V0UHJvdG9jb2wuUmVnaXN0ZXJQYWNrEiwKCUxvZ2luUGFjaxgFIAEoCzIZ",
            "LlNvY2tldFByb3RvY29sLkxvZ2luUGFjaxILCgNVaWQYBiABKAUSPgoSSW5p",
            "dFBsYXllckluZm9QYWNrGAcgASgLMiIuU29ja2V0UHJvdG9jb2wuSW5pdFBs",
            "YXllckluZm9QYWNrIkUKDFJlZ2lzdGVyUGFjaxIPCgdBY2NvdW50GAEgASgJ",
            "EhAKCFBhc3N3b3JkGAIgASgJEhIKClBsYXllck5hbWUYAyABKAkiOwoJTG9n",
            "aW5QYWNrEgsKA1VpZBgBIAEoBRIPCgdBY2NvdW50GAIgASgJEhAKCFBhc3N3",
            "b3JkGAMgASgJIo4BChJJbml0UGxheWVySW5mb1BhY2sSCwoDVWlkGAEgASgF",
            "EhIKClBsYXllck5hbWUYAiABKAkSDQoFTGV2ZWwYAyABKAUSDgoGTWF4RXhw",
            "GAQgASgCEhIKCkN1cnJlbnRFeHAYBSABKAISEQoJTWF4SGVhbHRoGAYgASgC",
            "EhEKCU1heFNoaWVsZBgHIAEoAio+CgtSZXF1ZXN0Q29kZRIPCgtSZXF1ZXN0",
            "Tm9uZRAAEggKBFVzZXIQARIKCgZHYW1pbmcQAhIICgRUZWFtEAMq6QEKCkFj",
            "dGlvbkNvZGUSDgoKQWN0aW9uTm9uZRAAEgwKCFJlZ2lzdGVyEAESCQoFTG9n",
            "aW4QAhISCg5Jbml0UGxheWVySW5mbxADEgwKCEpvaW5UZWFtEAQSDgoKVXBk",
            "YXRlVGVhbRAFEg0KCUxldmVsVGVhbRAGEhQKEFVwZGF0ZVBsYXllckluZm8Q",
            "BxIPCgtSZWFkeUF0dGFjaxAIEg8KC1N0YXJ0QXR0YWNrEAkSDQoJTmV4dEVu",
            "ZW15EAoSDQoJRW5kQXR0YWNrEAsSDAoIRXhpdEdhbWUQDBINCglCcmVha1Rl",
            "YW0QDSozCgpSZXR1cm5Db2RlEg4KClJldHVybk5vbmUQABILCgdTdWNjZXNz",
            "EAESCAoERmFpbBACYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::SocketProtocol.RequestCode), typeof(global::SocketProtocol.ActionCode), typeof(global::SocketProtocol.ReturnCode), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::SocketProtocol.MainPack), global::SocketProtocol.MainPack.Parser, new[]{ "RequestCode", "ActionCode", "ReturnCode", "RegisterPack", "LoginPack", "Uid", "InitPlayerInfoPack" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SocketProtocol.RegisterPack), global::SocketProtocol.RegisterPack.Parser, new[]{ "Account", "Password", "PlayerName" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SocketProtocol.LoginPack), global::SocketProtocol.LoginPack.Parser, new[]{ "Uid", "Account", "Password" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SocketProtocol.InitPlayerInfoPack), global::SocketProtocol.InitPlayerInfoPack.Parser, new[]{ "Uid", "PlayerName", "Level", "MaxExp", "CurrentExp", "MaxHealth", "MaxShield" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum RequestCode {
    [pbr::OriginalName("RequestNone")] RequestNone = 0,
    [pbr::OriginalName("User")] User = 1,
    [pbr::OriginalName("Gaming")] Gaming = 2,
    [pbr::OriginalName("Team")] Team = 3,
  }

  public enum ActionCode {
    [pbr::OriginalName("ActionNone")] ActionNone = 0,
    [pbr::OriginalName("Register")] Register = 1,
    [pbr::OriginalName("Login")] Login = 2,
    [pbr::OriginalName("InitPlayerInfo")] InitPlayerInfo = 3,
    [pbr::OriginalName("JoinTeam")] JoinTeam = 4,
    [pbr::OriginalName("UpdateTeam")] UpdateTeam = 5,
    [pbr::OriginalName("LevelTeam")] LevelTeam = 6,
    [pbr::OriginalName("UpdatePlayerInfo")] UpdatePlayerInfo = 7,
    [pbr::OriginalName("ReadyAttack")] ReadyAttack = 8,
    [pbr::OriginalName("StartAttack")] StartAttack = 9,
    [pbr::OriginalName("NextEnemy")] NextEnemy = 10,
    [pbr::OriginalName("EndAttack")] EndAttack = 11,
    [pbr::OriginalName("ExitGame")] ExitGame = 12,
    [pbr::OriginalName("BreakTeam")] BreakTeam = 13,
  }

  public enum ReturnCode {
    [pbr::OriginalName("ReturnNone")] ReturnNone = 0,
    [pbr::OriginalName("Success")] Success = 1,
    [pbr::OriginalName("Fail")] Fail = 2,
  }

  #endregion

  #region Messages
  public sealed partial class MainPack : pb::IMessage<MainPack> {
    private static readonly pb::MessageParser<MainPack> _parser = new pb::MessageParser<MainPack>(() => new MainPack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MainPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SocketProtocol.SocketProtocolReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MainPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MainPack(MainPack other) : this() {
      requestCode_ = other.requestCode_;
      actionCode_ = other.actionCode_;
      returnCode_ = other.returnCode_;
      registerPack_ = other.registerPack_ != null ? other.registerPack_.Clone() : null;
      loginPack_ = other.loginPack_ != null ? other.loginPack_.Clone() : null;
      uid_ = other.uid_;
      initPlayerInfoPack_ = other.initPlayerInfoPack_ != null ? other.initPlayerInfoPack_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MainPack Clone() {
      return new MainPack(this);
    }

    /// <summary>Field number for the "RequestCode" field.</summary>
    public const int RequestCodeFieldNumber = 1;
    private global::SocketProtocol.RequestCode requestCode_ = global::SocketProtocol.RequestCode.RequestNone;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.RequestCode RequestCode {
      get { return requestCode_; }
      set {
        requestCode_ = value;
      }
    }

    /// <summary>Field number for the "ActionCode" field.</summary>
    public const int ActionCodeFieldNumber = 2;
    private global::SocketProtocol.ActionCode actionCode_ = global::SocketProtocol.ActionCode.ActionNone;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.ActionCode ActionCode {
      get { return actionCode_; }
      set {
        actionCode_ = value;
      }
    }

    /// <summary>Field number for the "ReturnCode" field.</summary>
    public const int ReturnCodeFieldNumber = 3;
    private global::SocketProtocol.ReturnCode returnCode_ = global::SocketProtocol.ReturnCode.ReturnNone;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.ReturnCode ReturnCode {
      get { return returnCode_; }
      set {
        returnCode_ = value;
      }
    }

    /// <summary>Field number for the "RegisterPack" field.</summary>
    public const int RegisterPackFieldNumber = 4;
    private global::SocketProtocol.RegisterPack registerPack_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.RegisterPack RegisterPack {
      get { return registerPack_; }
      set {
        registerPack_ = value;
      }
    }

    /// <summary>Field number for the "LoginPack" field.</summary>
    public const int LoginPackFieldNumber = 5;
    private global::SocketProtocol.LoginPack loginPack_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.LoginPack LoginPack {
      get { return loginPack_; }
      set {
        loginPack_ = value;
      }
    }

    /// <summary>Field number for the "Uid" field.</summary>
    public const int UidFieldNumber = 6;
    private int uid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "InitPlayerInfoPack" field.</summary>
    public const int InitPlayerInfoPackFieldNumber = 7;
    private global::SocketProtocol.InitPlayerInfoPack initPlayerInfoPack_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SocketProtocol.InitPlayerInfoPack InitPlayerInfoPack {
      get { return initPlayerInfoPack_; }
      set {
        initPlayerInfoPack_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MainPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MainPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (RequestCode != other.RequestCode) return false;
      if (ActionCode != other.ActionCode) return false;
      if (ReturnCode != other.ReturnCode) return false;
      if (!object.Equals(RegisterPack, other.RegisterPack)) return false;
      if (!object.Equals(LoginPack, other.LoginPack)) return false;
      if (Uid != other.Uid) return false;
      if (!object.Equals(InitPlayerInfoPack, other.InitPlayerInfoPack)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (RequestCode != global::SocketProtocol.RequestCode.RequestNone) hash ^= RequestCode.GetHashCode();
      if (ActionCode != global::SocketProtocol.ActionCode.ActionNone) hash ^= ActionCode.GetHashCode();
      if (ReturnCode != global::SocketProtocol.ReturnCode.ReturnNone) hash ^= ReturnCode.GetHashCode();
      if (registerPack_ != null) hash ^= RegisterPack.GetHashCode();
      if (loginPack_ != null) hash ^= LoginPack.GetHashCode();
      if (Uid != 0) hash ^= Uid.GetHashCode();
      if (initPlayerInfoPack_ != null) hash ^= InitPlayerInfoPack.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RequestCode != global::SocketProtocol.RequestCode.RequestNone) {
        output.WriteRawTag(8);
        output.WriteEnum((int) RequestCode);
      }
      if (ActionCode != global::SocketProtocol.ActionCode.ActionNone) {
        output.WriteRawTag(16);
        output.WriteEnum((int) ActionCode);
      }
      if (ReturnCode != global::SocketProtocol.ReturnCode.ReturnNone) {
        output.WriteRawTag(24);
        output.WriteEnum((int) ReturnCode);
      }
      if (registerPack_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(RegisterPack);
      }
      if (loginPack_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(LoginPack);
      }
      if (Uid != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Uid);
      }
      if (initPlayerInfoPack_ != null) {
        output.WriteRawTag(58);
        output.WriteMessage(InitPlayerInfoPack);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RequestCode != global::SocketProtocol.RequestCode.RequestNone) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) RequestCode);
      }
      if (ActionCode != global::SocketProtocol.ActionCode.ActionNone) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ActionCode);
      }
      if (ReturnCode != global::SocketProtocol.ReturnCode.ReturnNone) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ReturnCode);
      }
      if (registerPack_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(RegisterPack);
      }
      if (loginPack_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(LoginPack);
      }
      if (Uid != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Uid);
      }
      if (initPlayerInfoPack_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(InitPlayerInfoPack);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MainPack other) {
      if (other == null) {
        return;
      }
      if (other.RequestCode != global::SocketProtocol.RequestCode.RequestNone) {
        RequestCode = other.RequestCode;
      }
      if (other.ActionCode != global::SocketProtocol.ActionCode.ActionNone) {
        ActionCode = other.ActionCode;
      }
      if (other.ReturnCode != global::SocketProtocol.ReturnCode.ReturnNone) {
        ReturnCode = other.ReturnCode;
      }
      if (other.registerPack_ != null) {
        if (registerPack_ == null) {
          RegisterPack = new global::SocketProtocol.RegisterPack();
        }
        RegisterPack.MergeFrom(other.RegisterPack);
      }
      if (other.loginPack_ != null) {
        if (loginPack_ == null) {
          LoginPack = new global::SocketProtocol.LoginPack();
        }
        LoginPack.MergeFrom(other.LoginPack);
      }
      if (other.Uid != 0) {
        Uid = other.Uid;
      }
      if (other.initPlayerInfoPack_ != null) {
        if (initPlayerInfoPack_ == null) {
          InitPlayerInfoPack = new global::SocketProtocol.InitPlayerInfoPack();
        }
        InitPlayerInfoPack.MergeFrom(other.InitPlayerInfoPack);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            RequestCode = (global::SocketProtocol.RequestCode) input.ReadEnum();
            break;
          }
          case 16: {
            ActionCode = (global::SocketProtocol.ActionCode) input.ReadEnum();
            break;
          }
          case 24: {
            ReturnCode = (global::SocketProtocol.ReturnCode) input.ReadEnum();
            break;
          }
          case 34: {
            if (registerPack_ == null) {
              RegisterPack = new global::SocketProtocol.RegisterPack();
            }
            input.ReadMessage(RegisterPack);
            break;
          }
          case 42: {
            if (loginPack_ == null) {
              LoginPack = new global::SocketProtocol.LoginPack();
            }
            input.ReadMessage(LoginPack);
            break;
          }
          case 48: {
            Uid = input.ReadInt32();
            break;
          }
          case 58: {
            if (initPlayerInfoPack_ == null) {
              InitPlayerInfoPack = new global::SocketProtocol.InitPlayerInfoPack();
            }
            input.ReadMessage(InitPlayerInfoPack);
            break;
          }
        }
      }
    }

  }

  public sealed partial class RegisterPack : pb::IMessage<RegisterPack> {
    private static readonly pb::MessageParser<RegisterPack> _parser = new pb::MessageParser<RegisterPack>(() => new RegisterPack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RegisterPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SocketProtocol.SocketProtocolReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterPack(RegisterPack other) : this() {
      account_ = other.account_;
      password_ = other.password_;
      playerName_ = other.playerName_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public RegisterPack Clone() {
      return new RegisterPack(this);
    }

    /// <summary>Field number for the "Account" field.</summary>
    public const int AccountFieldNumber = 1;
    private string account_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Account {
      get { return account_; }
      set {
        account_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "PlayerName" field.</summary>
    public const int PlayerNameFieldNumber = 3;
    private string playerName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PlayerName {
      get { return playerName_; }
      set {
        playerName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as RegisterPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(RegisterPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Account != other.Account) return false;
      if (Password != other.Password) return false;
      if (PlayerName != other.PlayerName) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Account.Length != 0) hash ^= Account.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      if (PlayerName.Length != 0) hash ^= PlayerName.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Account.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Account);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
      if (PlayerName.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(PlayerName);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Account.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Account);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      if (PlayerName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PlayerName);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(RegisterPack other) {
      if (other == null) {
        return;
      }
      if (other.Account.Length != 0) {
        Account = other.Account;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
      }
      if (other.PlayerName.Length != 0) {
        PlayerName = other.PlayerName;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Account = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
          case 26: {
            PlayerName = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class LoginPack : pb::IMessage<LoginPack> {
    private static readonly pb::MessageParser<LoginPack> _parser = new pb::MessageParser<LoginPack>(() => new LoginPack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LoginPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SocketProtocol.SocketProtocolReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LoginPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LoginPack(LoginPack other) : this() {
      uid_ = other.uid_;
      account_ = other.account_;
      password_ = other.password_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public LoginPack Clone() {
      return new LoginPack(this);
    }

    /// <summary>Field number for the "Uid" field.</summary>
    public const int UidFieldNumber = 1;
    private int uid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "Account" field.</summary>
    public const int AccountFieldNumber = 2;
    private string account_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Account {
      get { return account_; }
      set {
        account_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Password" field.</summary>
    public const int PasswordFieldNumber = 3;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as LoginPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(LoginPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Uid != other.Uid) return false;
      if (Account != other.Account) return false;
      if (Password != other.Password) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Uid != 0) hash ^= Uid.GetHashCode();
      if (Account.Length != 0) hash ^= Account.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Uid);
      }
      if (Account.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Account);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Password);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Uid != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Uid);
      }
      if (Account.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Account);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(LoginPack other) {
      if (other == null) {
        return;
      }
      if (other.Uid != 0) {
        Uid = other.Uid;
      }
      if (other.Account.Length != 0) {
        Account = other.Account;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Uid = input.ReadInt32();
            break;
          }
          case 18: {
            Account = input.ReadString();
            break;
          }
          case 26: {
            Password = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class InitPlayerInfoPack : pb::IMessage<InitPlayerInfoPack> {
    private static readonly pb::MessageParser<InitPlayerInfoPack> _parser = new pb::MessageParser<InitPlayerInfoPack>(() => new InitPlayerInfoPack());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<InitPlayerInfoPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SocketProtocol.SocketProtocolReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitPlayerInfoPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitPlayerInfoPack(InitPlayerInfoPack other) : this() {
      uid_ = other.uid_;
      playerName_ = other.playerName_;
      level_ = other.level_;
      maxExp_ = other.maxExp_;
      currentExp_ = other.currentExp_;
      maxHealth_ = other.maxHealth_;
      maxShield_ = other.maxShield_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitPlayerInfoPack Clone() {
      return new InitPlayerInfoPack(this);
    }

    /// <summary>Field number for the "Uid" field.</summary>
    public const int UidFieldNumber = 1;
    private int uid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Uid {
      get { return uid_; }
      set {
        uid_ = value;
      }
    }

    /// <summary>Field number for the "PlayerName" field.</summary>
    public const int PlayerNameFieldNumber = 2;
    private string playerName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PlayerName {
      get { return playerName_; }
      set {
        playerName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Level" field.</summary>
    public const int LevelFieldNumber = 3;
    private int level_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Level {
      get { return level_; }
      set {
        level_ = value;
      }
    }

    /// <summary>Field number for the "MaxExp" field.</summary>
    public const int MaxExpFieldNumber = 4;
    private float maxExp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float MaxExp {
      get { return maxExp_; }
      set {
        maxExp_ = value;
      }
    }

    /// <summary>Field number for the "CurrentExp" field.</summary>
    public const int CurrentExpFieldNumber = 5;
    private float currentExp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float CurrentExp {
      get { return currentExp_; }
      set {
        currentExp_ = value;
      }
    }

    /// <summary>Field number for the "MaxHealth" field.</summary>
    public const int MaxHealthFieldNumber = 6;
    private float maxHealth_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float MaxHealth {
      get { return maxHealth_; }
      set {
        maxHealth_ = value;
      }
    }

    /// <summary>Field number for the "MaxShield" field.</summary>
    public const int MaxShieldFieldNumber = 7;
    private float maxShield_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float MaxShield {
      get { return maxShield_; }
      set {
        maxShield_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as InitPlayerInfoPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(InitPlayerInfoPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Uid != other.Uid) return false;
      if (PlayerName != other.PlayerName) return false;
      if (Level != other.Level) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MaxExp, other.MaxExp)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(CurrentExp, other.CurrentExp)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MaxHealth, other.MaxHealth)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(MaxShield, other.MaxShield)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Uid != 0) hash ^= Uid.GetHashCode();
      if (PlayerName.Length != 0) hash ^= PlayerName.GetHashCode();
      if (Level != 0) hash ^= Level.GetHashCode();
      if (MaxExp != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MaxExp);
      if (CurrentExp != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(CurrentExp);
      if (MaxHealth != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MaxHealth);
      if (MaxShield != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(MaxShield);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Uid != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Uid);
      }
      if (PlayerName.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(PlayerName);
      }
      if (Level != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(Level);
      }
      if (MaxExp != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(MaxExp);
      }
      if (CurrentExp != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(CurrentExp);
      }
      if (MaxHealth != 0F) {
        output.WriteRawTag(53);
        output.WriteFloat(MaxHealth);
      }
      if (MaxShield != 0F) {
        output.WriteRawTag(61);
        output.WriteFloat(MaxShield);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Uid != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Uid);
      }
      if (PlayerName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PlayerName);
      }
      if (Level != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Level);
      }
      if (MaxExp != 0F) {
        size += 1 + 4;
      }
      if (CurrentExp != 0F) {
        size += 1 + 4;
      }
      if (MaxHealth != 0F) {
        size += 1 + 4;
      }
      if (MaxShield != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(InitPlayerInfoPack other) {
      if (other == null) {
        return;
      }
      if (other.Uid != 0) {
        Uid = other.Uid;
      }
      if (other.PlayerName.Length != 0) {
        PlayerName = other.PlayerName;
      }
      if (other.Level != 0) {
        Level = other.Level;
      }
      if (other.MaxExp != 0F) {
        MaxExp = other.MaxExp;
      }
      if (other.CurrentExp != 0F) {
        CurrentExp = other.CurrentExp;
      }
      if (other.MaxHealth != 0F) {
        MaxHealth = other.MaxHealth;
      }
      if (other.MaxShield != 0F) {
        MaxShield = other.MaxShield;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Uid = input.ReadInt32();
            break;
          }
          case 18: {
            PlayerName = input.ReadString();
            break;
          }
          case 24: {
            Level = input.ReadInt32();
            break;
          }
          case 37: {
            MaxExp = input.ReadFloat();
            break;
          }
          case 45: {
            CurrentExp = input.ReadFloat();
            break;
          }
          case 53: {
            MaxHealth = input.ReadFloat();
            break;
          }
          case 61: {
            MaxShield = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
