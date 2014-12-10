API文档
=======

认证方法
--------

```ruby
Param:
  access_key: API 秘钥为用户账户界面上生成的API KEY
  nonce: 随机数通常为UTC Time整数形式
  signature: 签名使用HMAC-SHA256方式是使用Secret Key 对 Nonce、 Username、Access Key进行签名的数据。签名时必需使用在API key产生的密钥生成HMAC-SHA256码.
```

##### Nonce

- Nonce is a regular integer number. It must be increasing with every request you make. Read more about it here. Example: if you set nonce to 1 in your first request, you must set it to at least 2 in your second request. You are not required to start with 1. A common practice is to use unix time for that parameter.

##### Signature

- Signature is a HMAC-SHA256 encoded message containing: nonce, client ID and API key. The HMAC-SHA256 code must be generated using a secret key that was generated with your API key. This code must be converted to it's hexadecimal representation (64 uppercase characters).
- Example (Python):

```ruby
  message = nonce + username + access_key
  signature = hmac.new(secret_key, msg=message, digestmod=hashlib.sha256).hexdigest()
```

例如： 获取当前所有开放市场URL为:

```ruby
http://localhost:3000/api/v1/currency_markets?access_key=PZMhwXRx75mNqGhl430mLUhqODzyhQSf1bAiB4Tf&nonce=1416974538344337&signature=e1e0051e8fe8aa681ca465567567aaff231f56db54d3d3fb7f49cbf17871097e
```

以下所有API均需要采用这样方式进行认证。

货币账户接口
------------

### 查询用户货币账户余额

```
HTTP METHOD: GET
HTTP URL: https://www.hashnest.com/api/v1/currency_accounts
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）

Return Result:
{

}
```

算力账户接口
------------

### 查询用户算力账户余额

```
HTTP METHOD: GET
HTTP URL: https://www.hashnest.com/api/v1/currency_accounts
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）

Return Result:
```

交易单接口
----------

### 查询用户现在的活跃订单

```
HTTP METHOD: GET
HTTP URL: https://www.hashnest.com/api/v1/orders
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  currency_market_id(交易市场ID): 1...
  category(订单类型): ["purchase" || "sale"]
```

### 查询用户成交订单

```
HTTP METHOD: GET
HTTP URL: http://www.hashnest.com/api/v1/orders/deal
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  currency_market_id: 1..
  page:
  page_amount:

Return Result:

```

### 创建一个委托单

```
HTTP METHOD： POST
HTTP URL: http://www.hashnest.com/api/v1/orders
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法)
  currency_market_id:
  amount:
  ppc:
  category:
  type:

Return Result:

```

### 撤销一个委托单

```
HTTP METHOD: GET
HTTP URL: http://www.hashnest.com/api/v1/orders/revoke
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  order_id:
```

### 一次撤销所有委托单

```
HTTP METHOD: GET
HTTP URL: http://www.hashnest.com/api/v1/orders/quic_revoke
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  currency_market_id:
  category:

Return Result:

```

开放市场接口
------------

### 所有开放市场

```
HTTP METHOD: GET
HTTP URL: https://www.hashnest.com/api/v1/currency_markets
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）

Return Result:
```

### 指定开放市场的成交单列表

```
HTTP METHOD: GET
HTTP URL: https://www.hashnest.com/api/v1/currency_markets/orders
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  currency_market_id:

Return Result:
```

账户流水信息
------------

### 查询流水详情

```
HTTP METHOD: POST
HTTP URL: https://www.hashnest.com/api/v1/transactions
Param:
  access_key: (见认证方法）
  nonce: (见认证方法）
  signature: (见认证方法）
  account_flow:
    currency:
    category:
    start_date:
    end_date:
  page:
  page_per_amount:

Return Result:
```
