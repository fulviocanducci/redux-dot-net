using Xunit;
using System;
using System.Collections.Generic;
using Redux;

namespace tests
{
  public class StoreValueProviderImplTests
  {
    private string key;
    private Store store;
    private StoreValueProviderImpl provider;

    public StoreValueProviderImplTests()
    {
      this.key = "object";
      HashSet<Reducer> reducers = new HashSet<Reducer>();
      reducers.Add(new ReducerImpl(this.key));
      this.store = new StoreImpl(reducers);

      this.provider = new StoreValueProviderImpl();
    }

    [Fact]
    public void should_implement()
    {
      Assert.IsAssignableFrom<StoreConsumer>(this.provider);
      Assert.IsAssignableFrom<KeyConsumer>(this.provider);
      Assert.IsAssignableFrom<StoreValueProvider>(this.provider);
    }

    [Fact]
    public void generic_get_should_return_string_value()
    {
      string value = "This is string value";
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      string actual = this.provider.get<string>();

      Assert.Equal(value, actual);
    }

    [Fact]
    public void typed_get_should_return_string_value()
    {
      string value = "This is string value";
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      object actual = this.provider.get(typeof(string));

      Assert.Equal(value, actual);
    }

    [Fact]
    public void generic_get_should_throw_if_value_type_differ()
    {
      string value = "This is string value";
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      Action notInteger = () => this.provider.get<int>();

      Assert.Throws<InvalidOperationException>(notInteger);
    }

    [Fact]
    public void typed_get_should_throw_if_value_type_differ()
    {
      string value = "This is string value";
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      Action notInteger = () => this.provider.get(typeof(int));

      Assert.Throws<InvalidOperationException>(notInteger);
    }

    [Fact]
    public void generic_get_should_throw_if_key_is_missing_in_Store()
    {
      this.provider.setStore(this.store);
      this.provider.setKey("user");
      Action noKey = () => this.provider.get<int>();

      Assert.Throws<InvalidOperationException>(noKey);
    }

    [Fact]
    public void typed_get_should_throw_if_key_is_missing_in_Store()
    {
      this.provider.setStore(this.store);
      this.provider.setKey("user");
      Action noKey = () => this.provider.get(typeof(int));

      Assert.Throws<InvalidOperationException>(noKey);
    }

    [Fact]
    public void generic_get_should_return_integer_value()
    {
      int value = 2938;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      int actual = this.provider.get<int>();

      Assert.Equal(value, actual);
    }

    [Fact]
    public void typed_get_should_return_integer_value()
    {
      int value = 2938;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      object actual = this.provider.get(typeof(int));

      Assert.Equal(value, actual);
    }

    [Fact]
    public void generic_get_should_return_boolean_value()
    {
      bool value = true;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      bool actual = this.provider.get<bool>();

      Assert.Equal(value, actual);
    }

    [Fact]
    public void typed_get_should_return_boolean_value()
    {
      bool value = true;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      object actual = this.provider.get(typeof(bool));

      Assert.Equal(value, actual);
    }

    [Fact]
    public void generic_get_should_return_DateTime_value()
    {
      DateTime value = DateTime.UtcNow;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      DateTime actual = this.provider.get<DateTime>();

      Assert.Equal(value, actual);
    }

    [Fact]
    public void typed_get_should_return_DateTime_value()
    {
      DateTime value = DateTime.UtcNow;
      Message message = new Message(this.key, value);
      this.store.Dispatch(message);

      this.provider.setStore(this.store);
      this.provider.setKey(this.key);
      object actual = this.provider.get(typeof(DateTime));

      Assert.Equal(value, actual);
    }

    [Fact]
    public void generic_get_should_throw_if_value_is_null()
    {
      this.provider.setStore(this.store);
      this.provider.setKey(this.key);

      Action getValue = () => this.provider.get<string>();
      InvalidOperationException error = 
        Assert.Throws<InvalidOperationException>(getValue);
      string expected = 
        $"There is no value of System.String type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);

      getValue = () => this.provider.get<int>();
      error = Assert.Throws<InvalidOperationException>(getValue);
      expected = 
        $"There is no value of System.Int32 type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);

      getValue = () => this.provider.get<bool>();
      error = Assert.Throws<InvalidOperationException>(getValue);
      expected = 
        $"There is no value of System.Boolean type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);
    }

    [Fact]
    public void typed_get_should_throw_if_value_is_null()
    {
      this.provider.setStore(this.store);
      this.provider.setKey(this.key);

      Action getValue = () => this.provider.get(typeof(string));
      InvalidOperationException error = 
        Assert.Throws<InvalidOperationException>(getValue);
      string expected = 
        $"There is no value of System.String type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);

      getValue = () => this.provider.get(typeof(int));
      error = Assert.Throws<InvalidOperationException>(getValue);
      expected = 
        $"There is no value of System.Int32 type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);

      getValue = () => this.provider.get(typeof(bool));
      error = Assert.Throws<InvalidOperationException>(getValue);
      expected = 
        $"There is no value of System.Boolean type found in cell with {this.key} key!";
      Assert.Equal(expected, error.Message);
    }
  }
}