using Better.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Better.Infrastructure.Data
{
    public class ChallengeContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ChallengeContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<CompositionCategory> CompositionCategories { get; set; } = null!;
        public virtual DbSet<CompositionSubcategory> CompositionSubcategories { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<CurrencyIndicator> CurrencyIndicators { get; set; } = null!;
        public virtual DbSet<FinancialEntity> FinancialEntities { get; set; } = null!;
        public virtual DbSet<Funding> Fundings { get; set; } = null!;
        public virtual DbSet<FundingComposition> FundingCompositions { get; set; } = null!;
        public virtual DbSet<FundingShareValue> FundingShareValues { get; set; } = null!;
        public virtual DbSet<Goal> Goals { get; set; } = null!;
        public virtual DbSet<GoalCategory> GoalCategories { get; set; } = null!;
        public virtual DbSet<GoalTransaction> GoalTransactions { get; set; } = null!;
        public virtual DbSet<GoalTransactionFunding> GoalTransactionFundings { get; set; } = null!;
        public virtual DbSet<InvestmentStrategy> InvestmentStrategies { get; set; } = null!;
        public virtual DbSet<InvestmentStrategyType> InvestmentStrategyTypes { get; set; } = null!;
        public virtual DbSet<Portfolio> Portfolios { get; set; } = null!;
        public virtual DbSet<PortfolioComposition> PortfolioCompositions { get; set; } = null!;
        public virtual DbSet<PortfolioFunding> PortfolioFundings { get; set; } = null!;
        public virtual DbSet<Risklevel> RiskLevels { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql(_configuration.GetConnectionString("SqlConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompositionCategory>(entity =>
            {
                entity.ToTable("compositioncategory");

                entity.HasIndex(e => e.Uuid, "uuidCompositionCategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<CompositionSubcategory>(entity =>
            {
                entity.ToTable("compositionsubcategory");

                entity.HasIndex(e => e.CategoryId, "idx_fk_compositionsubcategory_categoryid");

                entity.HasIndex(e => e.Uuid, "uuidCompositionSubcategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryId).HasColumnName("categoryid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CompositionSubcategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_compositionsubcategory_categoryid");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.HasIndex(e => e.Uuid, "currency_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.CurrencyCode).HasColumnName("currencycode");

                entity.Property(e => e.DigitsInfo).HasColumnName("digitsinfo");

                entity.Property(e => e.Display).HasColumnName("display");

                entity.Property(e => e.Locale).HasColumnName("locale");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.ServerFormatNumber).HasColumnName("serverformatnumber");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Yahoomnemonic).HasColumnName("yahoomnemonic");
            });

            modelBuilder.Entity<CurrencyIndicator>(entity =>
            {
                entity.ToTable("currencyindicator");

                entity.HasIndex(e => e.DestinationCurrencyId, "IX_currencyindicator_destinationcurrencyid");

                entity.HasIndex(e => new { e.SourceCurrencyId, e.DestinationCurrencyId, e.Date }, "currency_indicator_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ask).HasColumnName("ask");

                entity.Property(e => e.Bid).HasColumnName("bid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.DestinationCurrencyId).HasColumnName("destinationcurrencyid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Requestdate)
                    .HasPrecision(6)
                    .HasColumnName("requestdate")
                    .HasDefaultValueSql("'1970-01-01 00:00:00+00'::timestamp with time zone");

                entity.Property(e => e.SourceCurrencyId).HasColumnName("sourcecurrencyid");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.DestinationCurrency)
                    .WithMany(p => p.CurrencyIndicatorDestinationCurrencies)
                    .HasForeignKey(d => d.DestinationCurrencyId)
                    .HasConstraintName("fk_currency_indicator_destinationcurrencyid");

                entity.HasOne(d => d.SourceCurrency)
                    .WithMany(p => p.CurrencyIndicatorSourceCurrencies)
                    .HasForeignKey(d => d.SourceCurrencyId)
                    .HasConstraintName("fk_currency_indicator_sourcecurrencyid");
            });

            modelBuilder.Entity<FinancialEntity>(entity =>
            {
                entity.ToTable("financialentity");

                entity.HasIndex(e => e.DefaultcurrencyId, "IX_financialentity_defaultcurrencyid");

                entity.HasIndex(e => e.Uuid, "uuidFinancialEntity_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.DefaultcurrencyId).HasColumnName("defaultcurrencyid");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.HasOne(d => d.DefaultCurrency)
                    .WithMany(p => p.FinancialEntities)
                    .HasForeignKey(d => d.DefaultcurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Funding>(entity =>
            {
                entity.ToTable("funding");

                entity.HasIndex(e => e.CurrencyId, "IX_funding_currencyid");

                entity.HasIndex(e => e.FinancialEntityId, "idx_fk_funding_financialentityid");

                entity.HasIndex(e => e.Mnemonic, "mnemonicFunding_");

                entity.HasIndex(e => e.Uuid, "uuidFunding_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CmfUrl).HasColumnName("cmfurl");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyid");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.FinancialEntityId).HasColumnName("financialentityid");

                entity.Property(e => e.HasShareValue).HasColumnName("hassharevalue");

                entity.Property(e => e.IsBox).HasColumnName("isbox");

                entity.Property(e => e.Mnemonic).HasColumnName("mnemonic");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Yahoomnemonic).HasColumnName("yahoomnemonic");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Fundings)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.FinancialEntity)
                    .WithMany(p => p.Fundings)
                    .HasForeignKey(d => d.FinancialEntityId)
                    .HasConstraintName("fk_funding_financialentityid");
            });

            modelBuilder.Entity<FundingComposition>(entity =>
            {
                entity.ToTable("fundingcomposition");

                entity.HasIndex(e => e.FundingId, "idx_fk_fundingcomposition_fundingid");

                entity.HasIndex(e => e.SubcategoryId, "idx_fk_fundingcomposition_subcategoryid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.FundingId).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryid");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.FundingCompositions)
                    .HasForeignKey(d => d.FundingId)
                    .HasConstraintName("fk_fundingcomposition_fundingid");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.FundingCompositions)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("fk_portfoliocomposition_subcategoryid");
            });

            modelBuilder.Entity<FundingShareValue>(entity =>
            {
                entity.ToTable("fundingsharevalue");

                entity.HasIndex(e => new { e.FundingId, e.Date }, "funding_share_value_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.FundingId).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.FundingShareValues)
                    .HasForeignKey(d => d.FundingId)
                    .HasConstraintName("fk_fundingsharevalue_fundingid");
            });

            modelBuilder.Entity<Goal>(entity =>
            {
                entity.ToTable("goal");

                entity.HasIndex(e => e.CurrencyId, "IX_goal_currencyid");

                entity.HasIndex(e => e.DisplayCurrencyId, "IX_goal_displaycurrencyid");

                entity.HasIndex(e => e.RiskLevelId, "IX_goal_risklevelid");

                entity.HasIndex(e => e.FinancialEntityId, "goal_idx2");

                entity.HasIndex(e => e.GoalCategoryId, "idx_fk_goal_goalcategory");

                entity.HasIndex(e => e.PortfolioId, "idx_fk_goal_portfolioid");

                entity.HasIndex(e => e.UserId, "idx_fk_goal_userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyid");

                entity.Property(e => e.DisplayCurrencyId).HasColumnName("displaycurrencyid");

                entity.Property(e => e.FinancialEntityId).HasColumnName("financialentityid");

                entity.Property(e => e.GoalCategoryId).HasColumnName("goalcategoryid");

                entity.Property(e => e.InitialInvestment).HasColumnName("initialinvestment");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.MonthlyContribution).HasColumnName("monthlycontribution");

                entity.Property(e => e.PortfolioId).HasColumnName("portfolioid");

                entity.Property(e => e.RiskLevelId).HasColumnName("risklevelid");

                entity.Property(e => e.TargetAmount).HasColumnName("targetamount");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userid");

                entity.Property(e => e.Years).HasColumnName("years");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.GoalCurrencies)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.DisplayCurrency)
                    .WithMany(p => p.GoalDisplaycurrencies)
                    .HasForeignKey(d => d.DisplayCurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.FinancialEntity)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.FinancialEntityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_financialentityid");

                entity.HasOne(d => d.GoalCategory)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.GoalCategoryId)
                    .HasConstraintName("fk_goal_goalcategory");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.PortfolioId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_portfolioid");

                entity.HasOne(d => d.RiskLevel)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.RiskLevelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goal_risklevelid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_goal_userid");
            });

            modelBuilder.Entity<GoalCategory>(entity =>
            {
                entity.ToTable("goalcategory");

                entity.HasIndex(e => e.Uuid, "uuidGoalCategory_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");
            });

            modelBuilder.Entity<GoalTransaction>(entity =>
            {
                entity.ToTable("goaltransaction");

                entity.HasIndex(e => e.CurrencyId, "IX_goaltransaction_currencyid");

                entity.HasIndex(e => e.FinancialEntityId, "IX_goaltransaction_financialentityid");

                entity.HasIndex(e => e.GoalId, "idx_fk_goaltransaction_goalid");

                entity.HasIndex(e => e.OwnerId, "ix_goaltransaction_userid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.All).HasColumnName("all");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyid");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.FinancialEntityId).HasColumnName("financialentityid");

                entity.Property(e => e.GoalId).HasColumnName("goalid");

                entity.Property(e => e.IsProcessed).HasColumnName("isprocessed");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.OwnerId).HasColumnName("ownerid");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.GoalTransactions)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("fk_goaltransaction_currrencyid");

                entity.HasOne(d => d.FinancialEntity)
                    .WithMany(p => p.GoalTransactions)
                    .HasForeignKey(d => d.FinancialEntityId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransaction_financialentityid");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.GoalTransactions)
                    .HasForeignKey(d => d.GoalId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransaction_goalid");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.GoalTransactions)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("fk_goaltransaction_ownerid");
            });

            modelBuilder.Entity<GoalTransactionFunding>(entity =>
            {
                entity.ToTable("goaltransactionfunding");

                entity.HasIndex(e => new { e.GoalId, e.FundingId, e.State, e.Date }, "goaltransactionfunding_idx1");

                entity.HasIndex(e => new { e.GoalId, e.FundingId, e.State }, "goaltransactionfunding_idx2");

                entity.HasIndex(e => new { e.OwnerId, e.State }, "goaltransactionfunding_idx3");

                entity.HasIndex(e => e.OwnerId, "goaltransactionfunding_idx4");

                entity.HasIndex(e => e.FundingId, "idx_fk_goaltransactionfunding_fundingid");

                entity.HasIndex(e => e.TransactionId, "idx_fk_goaltransactionfunding_goaltransactionid");

                entity.HasIndex(e => e.GoalId, "ix_goaltransactionfunding_goalid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.FundingId).HasColumnName("fundingid");

                entity.Property(e => e.GoalId).HasColumnName("goalid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.OwnerId).HasColumnName("ownerid");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.Quotas).HasColumnName("quotas");

                entity.Property(e => e.State)
                    .HasColumnName("state")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.TransactionId).HasColumnName("transactionid");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.GoalTransactionFundings)
                    .HasForeignKey(d => d.FundingId)
                    .HasConstraintName("fk_goaltransactionfunding_fundingid");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.GoalTransactionFundings)
                    .HasForeignKey(d => d.GoalId)
                    .HasConstraintName("fk_goaltransactionfunding_goalid");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.GoalTransactionFundings)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("fk_goaltransactionfunding_ownerid");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.GoalTransactionFundings)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_goaltransactionfunding_goaltransactionid");
            });

            modelBuilder.Entity<InvestmentStrategy>(entity =>
            {
                entity.ToTable("investmentstrategy");

                entity.HasIndex(e => e.Code, "IX_investmentstrategy_code")
                    .IsUnique();

                entity.HasIndex(e => e.FinancialEntityId, "IX_investmentstrategy_financialentityid");

                entity.HasIndex(e => e.InvestmentStrategyTypeId, "IX_investmentstrategy_investmentstrategytypeid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DisplayOrder).HasColumnName("displayorder");

                entity.Property(e => e.Features).HasColumnName("features");

                entity.Property(e => e.FinancialEntityId).HasColumnName("financialentityid");

                entity.Property(e => e.IconUrl).HasColumnName("iconurl");

                entity.Property(e => e.InvestmentStrategyTypeId).HasColumnName("investmentstrategytypeid");

                entity.Property(e => e.IsDefault).HasColumnName("isdefault");

                entity.Property(e => e.IsRecommended).HasColumnName("isrecommended");

                entity.Property(e => e.IsVisible).HasColumnName("isvisible");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.RecommendedFor).HasColumnName("recommendedfor");

                entity.Property(e => e.ShortTitle)
                    .HasColumnName("shorttitle")
                    .HasDefaultValueSql("''::text");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.FinancialEntity)
                    .WithMany(p => p.InvestmentStrategies)
                    .HasForeignKey(d => d.FinancialEntityId)
                    .HasConstraintName("fk_investmentstrategy_financialentityid");

                entity.HasOne(d => d.InvestmentStrategyType)
                    .WithMany(p => p.InvestmentStrategies)
                    .HasForeignKey(d => d.InvestmentStrategyTypeId)
                    .HasConstraintName("FK_investmentstrategy_investmentstrategytype_investmentstrateg~");
            });

            modelBuilder.Entity<InvestmentStrategyType>(entity =>
            {
                entity.ToTable("investmentstrategytype");

                entity.HasIndex(e => e.FinancialentityId, "IX_investmentstrategytype_financialentityid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DisplayOrder).HasColumnName("displayorder");

                entity.Property(e => e.FinancialentityId).HasColumnName("financialentityid");

                entity.Property(e => e.IconUrl).HasColumnName("iconurl");

                entity.Property(e => e.IsDefault).HasColumnName("isdefault");

                entity.Property(e => e.IsVisible).HasColumnName("isvisible");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.RecommendedFor).HasColumnName("recommendedfor");

                entity.Property(e => e.ShortTitle).HasColumnName("shorttitle");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.FinancialEntity)
                    .WithMany(p => p.InvestmentStrategyTypes)
                    .HasForeignKey(d => d.FinancialentityId);
            });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.ToTable("portfolio");

                entity.HasIndex(e => e.InvestmentstrategyId, "IX_portfolio_investmentstrategyid");

                entity.HasIndex(e => e.RisklevelId, "IX_portfolio_risklevelid");

                entity.HasIndex(e => e.FinancialentityId, "portfolio_idx2");

                entity.HasIndex(e => e.Uuid, "uuidPortfolio_")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BpComission).HasColumnName("bpcomission");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EstimatedProfitability).HasColumnName("estimatedprofitability");

                entity.Property(e => e.ExtraProfitabilityCurrencyCode).HasColumnName("extraprofitabilitycurrencycode");

                entity.Property(e => e.FinancialentityId).HasColumnName("financialentityid");

                entity.Property(e => e.InvestmentstrategyId).HasColumnName("investmentstrategyid");

                entity.Property(e => e.IsDefault).HasColumnName("isdefault");

                entity.Property(e => e.Maxrangeyear).HasColumnName("maxrangeyear");

                entity.Property(e => e.Minrangeyear).HasColumnName("minrangeyear");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Profitability)
                    .HasColumnType("json")
                    .HasColumnName("profitability");

                entity.Property(e => e.RisklevelId).HasColumnName("risklevelid");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.Financialentity)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.FinancialentityId)
                    .HasConstraintName("fk_portfolio_financialentityid");

                entity.HasOne(d => d.Investmentstrategy)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.InvestmentstrategyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_portfolio_investmentstraegyid");

                entity.HasOne(d => d.Risklevel)
                    .WithMany(p => p.Portfolios)
                    .HasForeignKey(d => d.RisklevelId)
                    .HasConstraintName("fk_portfolio_risklevelid");
            });

            modelBuilder.Entity<PortfolioComposition>(entity =>
            {
                entity.ToTable("portfoliocomposition");

                entity.HasIndex(e => e.PortfolioId, "idx_fk_portfoliocomposition_portfolioid");

                entity.HasIndex(e => e.SubcategoryId, "idx_fk_portfoliocomposition_subcategoryid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.PortfolioId).HasColumnName("portfolioid");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryid");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.PortfolioCompositions)
                    .HasForeignKey(d => d.PortfolioId)
                    .HasConstraintName("fk_portfoliofunding_portfolioid");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.PortfolioCompositions)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("fk_portfoliocomposition_subcategoryid");
            });

            modelBuilder.Entity<PortfolioFunding>(entity =>
            {
                entity.ToTable("portfoliofunding");

                entity.HasIndex(e => e.FundingId, "idx_fk_portfoliofunding_fundingid");

                entity.HasIndex(e => e.PortfolioId, "portfoliofunding_idx2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.FundingId).HasColumnName("fundingid");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Percentage).HasColumnName("percentage");

                entity.Property(e => e.PortfolioId).HasColumnName("portfolioid");

                entity.HasOne(d => d.Funding)
                    .WithMany(p => p.PortfolioFundings)
                    .HasForeignKey(d => d.FundingId)
                    .HasConstraintName("fk_portfoliofunding_fundingid");

                entity.HasOne(d => d.Portfolio)
                    .WithMany(p => p.PortfolioFundings)
                    .HasForeignKey(d => d.PortfolioId)
                    .HasConstraintName("fk_portfoliofunding_portfolioid");
            });

            modelBuilder.Entity<Risklevel>(entity =>
            {
                entity.ToTable("risklevel");

                entity.HasIndex(e => e.Code, "IX_risklevel_code")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.CurrencyId, "IX_user_currencyid");

                entity.HasIndex(e => e.AdvisorId, "idx_fk_user_advisorid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdvisorId).HasColumnName("advisorid");

                entity.Property(e => e.Created)
                    .HasPrecision(6)
                    .HasColumnName("created");

                entity.Property(e => e.CurrencyId).HasColumnName("currencyid");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Modified)
                    .HasPrecision(6)
                    .HasColumnName("modified");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.HasOne(d => d.Advisor)
                    .WithMany(p => p.InverseAdvisor)
                    .HasForeignKey(d => d.AdvisorId)
                    .HasConstraintName("fk_user_advisorid");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}