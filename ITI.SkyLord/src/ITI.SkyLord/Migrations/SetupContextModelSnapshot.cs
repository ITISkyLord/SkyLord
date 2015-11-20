using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.Migrations
{
    [DbContext(typeof(SetupContext))]
    partial class SetupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.EPlayer", b =>
                {
                    b.Property<long>("PlayerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("WorldWorldId");

                    b.HasKey("PlayerId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.EProfil", b =>
                {
                    b.Property<long>("ProfilId");

                    b.Property<string>("Description");

                    b.Property<string>("Mail");

                    b.Property<string>("Password");

                    b.HasKey("ProfilId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.EWorld", b =>
                {
                    b.Property<long>("WorldId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("WorldId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.EPlayer", b =>
                {
                    b.HasOne("ITI.SkyLord.Models.Entity_Framework.Entites.EWorld")
                        .WithMany()
                        .ForeignKey("WorldWorldId");
                });

            modelBuilder.Entity("ITI.SkyLord.Models.Entity_Framework.Entites.EProfil", b =>
                {
                    b.HasOne("ITI.SkyLord.Models.Entity_Framework.Entites.EPlayer")
                        .WithOne()
                        .ForeignKey("ITI.SkyLord.Models.Entity_Framework.Entites.EProfil", "ProfilId");
                });
        }
    }
}
