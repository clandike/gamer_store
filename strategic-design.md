flowchart LR
    subgraph Core
      O[Ordering]
      P[Pricing]
    end
    subgraph Supporting
      C[Catalog]
      I[Inventory]
    end
    subgraph Generic
      S[Shipping]
    end

    C -- "Customer/Supplier" --> O
    C <-- "Shared Kernel: SKU" --> I
    O -- "event: OrderPlaced (C/S)" --> I
    O -- "sync call (Conformist)" --> P
    P -- "ACL: Category ref" --> C
    O -- "event: OrderConfirmed (Published Language)" --> S
